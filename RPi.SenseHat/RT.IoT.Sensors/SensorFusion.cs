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
	public abstract class SensorFusion
	{
		/// <summary>
		/// The slerp power valule controls the influence of the measured state to correct the predicted state
		/// 0 = measured state ignored (just gyros), 1 = measured state overrides predicted state.
		/// In between 0 and 1 mixes the two conditions
		/// </summary>
		public double SlerpPower
		{ get; set; } = 0.02;

		/// <summary>
		/// Change this value to set the magnetic declination adjustment
		/// </summary>
		public double CompassAdjDeclination
		{ get; set; } = 0;

		/// <summary>
		/// If true, gyro data used
		/// </summary>
		public bool EnableGyro
		{ get; set; } = true;

		/// <summary>
		/// If true, accel data used
		/// </summary>
		public bool EnableAcceleration
		{ get; set; } = true;

		/// <summary>
		/// If true, mag data used
		/// </summary>
		public bool EnableMagneticField
		{ get; set; } = true;

		protected bool FirstTime = true;

		protected static readonly Quaternion Gravity = new Quaternion(0, 0, 0, 1);

		protected Quaternion StateQ;

		protected TimeSpan TimeDelta;

		protected DateTime LastFusionTime;

		protected Vector3 MeasuredPose;
		protected Quaternion MeasuredQPose;
		protected Vector3 FusionPose;
		protected Quaternion FusionQPose;

		protected Vector3? Gyro;
		protected Vector3? Acceleration;
		protected Vector3? MagneticField;

		protected int SampleNumber;

		protected SensorFusion()
		{
		}

		public virtual void Reset()
		{
			FirstTime = true;
			FusionPose = new Vector3();
			FusionQPose.FromEuler(FusionPose);
			Gyro = new Vector3();
			Acceleration = new Vector3();
			MagneticField = new Vector3();
			MeasuredPose = new Vector3();
			MeasuredQPose.FromEuler(MeasuredPose);
			SampleNumber = 0;
		}

		internal abstract void ProcessNewImuReadings(ref SensorReadings imuReadings);

		protected void CalculatePose(Vector3? acceleration, Vector3? magneticField, double magDeclination)
		{
			if (acceleration.HasValue)
			{
				MeasuredPose = acceleration.Value.AccelToEuler();
			}
			else
			{
				MeasuredPose = FusionPose;
				MeasuredPose.Z = 0;
			}

			if (magneticField.HasValue)
			{
				var q = new Quaternion();
				q.FromEuler(MeasuredPose);
				var m = new Quaternion(0, magneticField.Value.X, magneticField.Value.Y, magneticField.Value.Z);
				m = q * m * q.Conjugate();
				MeasuredPose.Z = -Math.Atan2(m.Y, m.X) - magDeclination;
			}
			else
			{
				MeasuredPose.Z = FusionPose.Z;
			}

			MeasuredQPose.FromEuler(MeasuredPose);

			//  check for quaternion aliasing. If the quaternion has the wrong sign
			//  the filter will be very unhappy.

			int maxIndex = -1;
			double maxVal = -1000;

			for (int i = 0; i < 4; i++)
			{
				if (Math.Abs(MeasuredQPose.GetData(i)) > maxVal)
				{
					maxVal = Math.Abs(MeasuredQPose.GetData(i));
					maxIndex = i;
				}
			}

			//  if the biggest component has a different sign in the measured and kalman poses,
			//  change the sign of the measured pose to match.

			if (((MeasuredQPose.GetData(maxIndex) < 0) && (FusionQPose.GetData(maxIndex) > 0)) ||
				((MeasuredQPose.GetData(maxIndex) > 0) && (FusionQPose.GetData(maxIndex) < 0)))
			{
				MeasuredQPose.Scalar = -MeasuredQPose.Scalar;
				MeasuredQPose.X = -MeasuredQPose.X;
				MeasuredQPose.Y = -MeasuredQPose.Y;
				MeasuredQPose.Z = -MeasuredQPose.Z;
				MeasuredQPose.ToEuler(out MeasuredPose);
			}
		}

		/// <summary>
		/// Get acceleration residuals.
		/// </summary>
		public Vector3 AccelerationResiduals
		{
			get
			{
				if (!Acceleration.HasValue)
				{
					return new Vector3();
				}

				// do gravity rotation and subtraction

				// create the conjugate of the pose

				Quaternion fusedConjugate = FusionQPose.Conjugate();

				// now do the rotation - takes two steps with qTemp as the intermediate variable

				Quaternion qTemp = Gravity * FusionQPose;
				Quaternion rotatedGravity = fusedConjugate * qTemp;

				// now adjust the measured accel and change the signs to make sense

				return new Vector3(
					-(Acceleration.Value.X - rotatedGravity.X),
					-(Acceleration.Value.Y - rotatedGravity.Y),
					-(Acceleration.Value.Z - rotatedGravity.Z));
			}
		}
	}
}
