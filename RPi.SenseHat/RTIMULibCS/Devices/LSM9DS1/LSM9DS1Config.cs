
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

namespace RichardsTech.Sensors.Devices.LSM9DS1
{
	/// <summary>
	/// Configuration of the LSM9DS1 IMU-sensor.
	/// </summary>
	public class LSM9DS1Config
	{
		public LSM9DS1Config()
		{
			GyroSampleRate = GyroSampleRate.Freq119Hz;
			GyroBandwidthCode = GyroBandwidthCode.BandwidthCode1;
			GyroHighPassFilterCode = GyroHighPassFilterCode.FilterCode4;
			GyroFullScaleRange = GyroFullScaleRange.Range500;

			AccelSampleRate = AccelSampleRate.Freq119Hz;
			AccelFullScaleRange = AccelFullScaleRange.Range8g;
			AccelLowPassFilter = AccelLowPassFilter.Freq50Hz;

			CompassSampleRate = CompassSampleRate.Freq20Hz;
			MagneticFullScaleRange = MagneticFullScaleRange.Range4Gauss;
		}

		/// <summary>
		/// The gyro sample rate
		/// </summary>
		public GyroSampleRate GyroSampleRate
		{ get; private set; }

		/// <summary>
		/// The gyro bandwidth code
		/// </summary>
		public GyroBandwidthCode GyroBandwidthCode
		{ get; private set; }

		/// <summary>
		/// The gyro high pass filter cutoff code
		/// </summary>
		public GyroHighPassFilterCode GyroHighPassFilterCode
		{ get; private set; }

		/// <summary>
		/// The gyro full scale range
		/// </summary>
		public GyroFullScaleRange GyroFullScaleRange
		{ get; private set; }

		/// <summary>
		/// The accel sample rate
		/// </summary>
		public AccelSampleRate AccelSampleRate
		{ get; private set; }

		/// <summary>
		/// The accel full scale range
		/// </summary>
		public AccelFullScaleRange AccelFullScaleRange
		{ get; private set; }

		/// <summary>
		/// The accel low pass filter
		/// </summary>
		public AccelLowPassFilter AccelLowPassFilter
		{ get; private set; }

		/// <summary>
		/// The compass sample rate
		/// </summary>
		public CompassSampleRate CompassSampleRate
		{ get; private set; }

		/// <summary>
		/// The compass full scale range
		/// </summary>
		public MagneticFullScaleRange MagneticFullScaleRange
		{ get; private set; }
	}
}