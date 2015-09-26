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
	/// The various values from a sensor reading.
	/// </summary>
	public struct SensorReadings
	{
		/// <summary>
		/// Sample timestamp
		/// </summary>
		public DateTime Timestamp;

		/// <summary>
		/// True if fusion pose valid
		/// </summary>
		public bool FusionPoseValid;

		/// <summary>
		/// The fusion pose
		/// </summary>
		public Vector3 FusionPose;

		/// <summary>
		/// True if the fusion quaternion is valid
		/// </summary>
		public bool FusionQPoseValid;

		/// <summary>
		/// The fusion quaternion
		/// </summary>
		public Quaternion FusionQPose;

		/// <summary>
		/// True if gyro data is valid
		/// </summary>
		public bool GyroValid;

		/// <summary>
		/// Gyro data in radians/sec
		/// </summary>
		public Vector3 Gyro;

		/// <summary>
		/// True if accel data valid
		/// </summary>
		public bool AccelerationValid;

		/// <summary>
		/// Acceleration data in g
		/// </summary>
		public Vector3 Acceleration;

		/// <summary>
		/// True if mag data valid
		/// </summary>
		public bool MagneticFieldValid;

		/// <summary>
		/// Magnetic field vector in uT
		/// </summary>
		public Vector3 MagneticField;

		/// <summary>
		/// True if pressure data valid
		/// </summary>
		public bool PressureValid;

		/// <summary>
		/// Pressure in hPa
		/// </summary>
		public double Pressure;

		/// <summary>
		/// True if temperature data valid
		/// </summary>
		public bool TemperatureValid;

		/// <summary>
		/// Temperature in degree C
		/// </summary>
		public double Temperature;

		/// <summary>
		/// True if humidity data valid
		/// </summary>
		public bool HumidityValid;

		/// <summary>
		/// Relative humidity in %RH
		/// </summary>
		public double Humidity;
	}
}