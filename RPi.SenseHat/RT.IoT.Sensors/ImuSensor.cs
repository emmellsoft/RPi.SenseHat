////////////////////////////////////////////////////////////////////////////
//
//  This file is part of RTIMULibCS
//
//  Copyright (c) 2015, richards-tech, LLC
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of 
//  this software and associated documentation files (the "Software"), to deal in 
//  the Software without restriction, including without limitation the rights to use, 
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
//  Software, and to permit persons to whom the Software is furnished to do so, 
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all 
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace RichardsTech.Sensors
{
	/// <summary>
	/// A generic IMU sensor.
	/// </summary>
	public abstract class ImuSensor : Sensor
	{
		private double _gyroLearningAlpha;                        // gyro bias rapid learning rate
		private double _gyroContinuousAlpha;                      // gyro bias continuous (slow) learning rate
		private int _gyroSampleCount;                             // number of gyro samples used

		private Vector3 _previousAccel;                           // previous step accel for gyro learning
		private Vector3 _gyroBias;                                // previous step gyro bias for gyro learning
		private Vector3 _magAverage;                              // a running average to smooth the mag outputs

		private readonly double[] _magMax = new double[3];        // max values seen for mag
		private readonly double[] _magMin = new double[3];        // min values seen for mag
		private double _magMaxDelta;                              // max difference between max and min used for scale
		private readonly double[] _magCalScale = new double[3];   // used to scale the mag readings
		private readonly double[] _magCalOffset = new double[3];  // used to offset the mag readings

		protected int SampleRate;                                 // samples per second
		protected long SampleInterval;                            // interval between samples in microseonds

		protected ImuSensor(SensorFusion fusion)
		{
			Fusion = fusion;

			AxisRotation = new AxisRotation();

			for (int i = 0; i < 3; i++)
			{
				_magMax[i] = -1000.0;
				_magMin[i] = 1000.0;
			}
		}

		/// <summary>
		/// Sets the learning rate for compass running average calculation
		/// </summary>
		public double CompassAlpha
		{ get; set; } = 0.2;

		/// <summary>
		/// Defines the accelerometer noise level
		/// </summary>
		public double FuzzyGyroZero
		{ get; set; } = 0.20;

		/// <summary>
		/// Defines the accelerometer noise level
		/// </summary>
		public double FuzzyAccelZero
		{ get; set; } = 0.05;

		/// <summary>
		/// If we have mag cal valid data
		/// </summary>
		public bool MagCalValid
		{ get; private set; }

		/// <summary>
		/// True if valid gyro bias has been obtained
		/// </summary>
		public bool GyroBiasValid
		{ get; private set; }

		/// <summary>
		/// The axis rotation
		/// </summary>
		public AxisRotation AxisRotation
		{ get; }

		/// <summary>
		/// The sensor fusion support.
		/// </summary>
		public SensorFusion Fusion
		{ get; }

		protected override void AfterInitDevice()
		{
			base.AfterInitDevice();

			GyroBiasInit();
		}

		private void GyroBiasInit()
		{
			if (SampleRate != 0)
			{
				_gyroLearningAlpha = 2.0f / SampleRate;
				_gyroContinuousAlpha = 0.01f / SampleRate;
			}

			_gyroSampleCount = 0;
		}

		public void SetGyroContinuousLearningAlpha(double alpha)
		{
			if ((alpha < 0.0) || (alpha >= 1.0))
			{
				throw new SensorException("Alpha out of range (0..1 allowed)");
			}

			_gyroContinuousAlpha = alpha;
		}

		protected override void ProcessReadings(ref SensorReadings readings)
		{
			PerformAxisRotation(ref readings);

			HandleGyroBias(ref readings);

			CalibrateAverageCompass(ref readings);

			Fusion.ProcessNewImuReadings(ref readings);
		}

		private void PerformAxisRotation(ref SensorReadings readings)
		{
			if (readings.GyroValid)
			{
				readings.Gyro = AxisRotation.Rotate(readings.Gyro);
			}

			if (readings.AccelerationValid)
			{
				readings.Acceleration = AxisRotation.Rotate(readings.Acceleration);
			}

			if (readings.MagneticFieldValid)
			{
				readings.MagneticField = AxisRotation.Rotate(readings.MagneticField);
			}
		}

		private void HandleGyroBias(ref SensorReadings readings)
		{
			if (readings.AccelerationValid)
			{
				Vector3 deltaAccel = _previousAccel;
				deltaAccel -= readings.Acceleration; // compute difference
				_previousAccel = readings.Acceleration;

				if (readings.GyroValid)
				{
					if ((deltaAccel.Length() < FuzzyAccelZero) && (readings.Gyro.Length() < FuzzyGyroZero))
					{
						// what we are seeing on the gyros should be bias only so learn from this

						if (_gyroSampleCount < (5 * SampleRate))
						{
							_gyroBias.X = (1.0 - _gyroLearningAlpha) * _gyroBias.X + _gyroLearningAlpha * readings.Gyro.X;
							_gyroBias.Y = (1.0 - _gyroLearningAlpha) * _gyroBias.Y + _gyroLearningAlpha * readings.Gyro.Y;
							_gyroBias.Z = (1.0 - _gyroLearningAlpha) * _gyroBias.Z + _gyroLearningAlpha * readings.Gyro.Z;

							_gyroSampleCount++;

							if (_gyroSampleCount == (5 * SampleRate))
							{
								// this could have been true already of course
								GyroBiasValid = true;
							}
						}
						else
						{
							_gyroBias.X = (1.0 - _gyroContinuousAlpha) * _gyroBias.X + _gyroContinuousAlpha * readings.Gyro.X;
							_gyroBias.Y = (1.0 - _gyroContinuousAlpha) * _gyroBias.Y + _gyroContinuousAlpha * readings.Gyro.Y;
							_gyroBias.Z = (1.0 - _gyroContinuousAlpha) * _gyroBias.Z + _gyroContinuousAlpha * readings.Gyro.Z;
						}
					}

					readings.Gyro -= _gyroBias;
				}
			}
		}

		private void CalibrateAverageCompass(ref SensorReadings readings)
		{
			//  calibrate if required

			SetCalibrationData(readings);

			if (readings.MagneticFieldValid)
			{
				if (MagCalValid)
				{
					readings.MagneticField.X = (readings.MagneticField.X - _magCalOffset[0]) * _magCalScale[0];
					readings.MagneticField.Y = (readings.MagneticField.Y - _magCalOffset[1]) * _magCalScale[1];
					readings.MagneticField.Z = (readings.MagneticField.Z - _magCalOffset[2]) * _magCalScale[2];
				}

				//  update running average

				_magAverage.X = readings.MagneticField.X * CompassAlpha + _magAverage.X * (1.0 - CompassAlpha);
				_magAverage.Y = readings.MagneticField.Y * CompassAlpha + _magAverage.Y * (1.0 - CompassAlpha);
				_magAverage.Z = readings.MagneticField.Z * CompassAlpha + _magAverage.Z * (1.0 - CompassAlpha);

				readings.MagneticField = _magAverage;
			}
		}

		private void SetCalibrationData(SensorReadings readings)
		{
			if (!readings.MagneticFieldValid)
			{
				return;
			}

			bool changed = false;

			// see if there is a new max or min

			if (_magMax[0] < readings.MagneticField.X)
			{
				_magMax[0] = readings.MagneticField.X;
				changed = true;
			}

			if (_magMax[1] < readings.MagneticField.Y)
			{
				_magMax[1] = readings.MagneticField.Y;
				changed = true;
			}

			if (_magMax[2] < readings.MagneticField.Z)
			{
				_magMax[2] = readings.MagneticField.Z;
				changed = true;
			}

			if (_magMin[0] > readings.MagneticField.X)
			{
				_magMin[0] = readings.MagneticField.X;
				changed = true;
			}

			if (_magMin[1] > readings.MagneticField.Y)
			{
				_magMin[1] = readings.MagneticField.Y;
				changed = true;
			}

			if (_magMin[2] > readings.MagneticField.Z)
			{
				_magMin[2] = readings.MagneticField.Z;
				changed = true;
			}

			if (!changed)
			{
				return;
			}

			double delta;

			if (!MagCalValid)
			{
				MagCalValid = true;

				for (int i = 0; i < 3; i++)
				{
					delta = _magMax[i] - _magMin[i];
					if ((delta < 30) || (_magMin[i] > 0) || (_magMax[i] < 0))
					{
						MagCalValid = false;
						break;
					}
				}
			}

			if (MagCalValid)
			{
				_magMaxDelta = -1;

				for (int i = 0; i < 3; i++)
				{
					if ((_magMax[i] - _magMin[i]) > _magMaxDelta)
					{
						_magMaxDelta = _magMax[i] - _magMin[i];
					}
				}

				// adjust for + and - range

				_magMaxDelta /= 2.0;
			}

			for (int i = 0; i < 3; i++)
			{
				delta = (_magMax[i] - _magMin[i]) / 2.0;
				_magCalScale[i] = _magMaxDelta / delta;
				_magCalOffset[i] = (_magMax[i] + _magMin[i]) / 2.0;
			}
		}

		/// <summary>
		/// Assign a new reading from an external source
		/// </summary>
		public void SetExtData(
			double gx,
			double gy,
			double gz,
			double ax,
			double ay,
			double az,
			double mx,
			double my,
			double mz,
			DateTime timestamp)
		{
			var readings = new SensorReadings
			{
				Gyro = new Vector3(gx, gy, gz),
				Acceleration = new Vector3(ax, ay, az),
				MagneticField = new Vector3(mx, my, mz),
				Timestamp = timestamp
			};

			AssignNewReadings(readings, false);
		}
	}
}