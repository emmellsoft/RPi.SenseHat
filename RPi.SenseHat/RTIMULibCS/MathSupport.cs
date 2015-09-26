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
	/// Math support for the sensors
	/// </summary>
	public static class MathSupport
	{
		public const double DegreeToRad = Math.PI / 180.0;

		public const double RadToDegree = 180.0 / Math.PI;

		//  convertPressureToHeight() - the conversion uses the formula:
		//
		//  h = (T0 / L0) * ((p / P0)**(-(R* * L0) / (g0 * M)) - 1)
		//
		//  where:
		//  h  = height above sea level
		//  T0 = standard temperature at sea level = 288.15
		//  L0 = standard temperatur elapse rate = -0.0065
		//  p  = measured pressure
		//  P0 = static pressure = 1013.25 (but can be overridden)
		//  g0 = gravitational acceleration = 9.80665
		//  M  = mloecular mass of earth's air = 0.0289644
		//  R* = universal gas constant = 8.31432
		//
		//  Given the constants, this works out to:
		//
		//  h = 44330.8 * (1 - (p / P0)**0.190263)

		public static double ConvertPressureToHeight(double pressure, double staticPressure)
		{
			return 44330.8 * (1 - Math.Pow(pressure / staticPressure, (double)0.190263));
		}

		public static Vector3 PoseFromAccelMag(Vector3 accel, Vector3 mag)
		{
			Vector3 result = accel.AccelToEuler();

			//  q.fromEuler(result);
			//  since result.z() is always 0, this can be optimized a little

			double cosX2 = Math.Cos(result.X / 2.0f);
			double sinX2 = Math.Sin(result.X / 2.0f);
			double cosY2 = Math.Cos(result.Y / 2.0f);
			double sinY2 = Math.Sin(result.Y / 2.0f);

			Quaternion q = new Quaternion(cosX2 * cosY2, sinX2 * cosY2, cosX2 * sinY2, -sinX2 * sinY2);
			Quaternion m = new Quaternion(0, mag.X, mag.Y, mag.Z);

			m = q * m * q.Conjugate();
			result.Z = -Math.Atan2(m.Y, m.X);
			return result;
		}

		public static Vector3 ConvertToVector(byte[] rawData, double scale, ByteOrder byteOrder)
		{
			switch (byteOrder)
			{
				case ByteOrder.BigEndian:
					return new Vector3(
						((UInt16)(((UInt16)rawData[0] << 8) | (UInt16)rawData[1])) * scale,
						((Int16)(((UInt16)rawData[2] << 8) | (UInt16)rawData[3])) * scale,
						((Int16)(((UInt16)rawData[4] << 8) | (UInt16)rawData[5])) * scale);

				case ByteOrder.LittleEndian:
					return new Vector3(
						((Int16)(((UInt16)rawData[1] << 8) | (UInt16)rawData[0])) * scale,
						((Int16)(((UInt16)rawData[3] << 8) | (UInt16)rawData[2])) * scale,
						((Int16)(((UInt16)rawData[5] << 8) | (UInt16)rawData[4])) * scale);

				default:
					throw new SensorException($"Unsupported byte order {byteOrder}");
			}
		}
	}
}

