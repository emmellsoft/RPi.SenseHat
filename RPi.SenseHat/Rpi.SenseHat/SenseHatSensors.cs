////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat
//
//  Copyright (c) 2017, Mattias Larsson
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
using RichardsTech.Sensors;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHatSensors : ISenseHatSensors, IDisposable
	{
		public SenseHatSensors(
			ImuSensor imuSensor,
			PressureSensor pressureSensor,
			HumiditySensor humiditySensor)
		{
			ImuSensor = imuSensor;
			PressureSensor = pressureSensor;
			HumiditySensor = humiditySensor;

			ImuSensor.OnReadingsChanged += (s, e) => ImuReadingsChanged();
			PressureSensor.OnReadingsChanged += (s, e) => PressureReadingsChanged();
			HumiditySensor.OnReadingsChanged += (s, e) => HumidityReadingsChanged();
		}

		void IDisposable.Dispose()
		{
			ImuSensor.Dispose();
			PressureSensor.Dispose();
			HumiditySensor.Dispose();
		}

		public Vector3? Gyro
		{ get; private set; }

		public Vector3? Acceleration
		{ get; private set; }

		public Vector3? MagneticField
		{ get; private set; }

		public Vector3? Pose
		{ get; private set; }

		public double? Pressure
		{ get; private set; }

		public double? Temperature
		{ get; private set; }

		public double? Humidity
		{ get; private set; }

		public ImuSensor ImuSensor
		{ get; }

		public PressureSensor PressureSensor
		{ get; }

		public HumiditySensor HumiditySensor
		{ get; }

		private void ImuReadingsChanged()
		{
			if (ImuSensor.Readings.GyroValid)
			{
				Gyro = ImuSensor.Readings.Gyro;
			}

			if (ImuSensor.Readings.AccelerationValid)
			{
				Acceleration = ImuSensor.Readings.Acceleration;
			}

			if (ImuSensor.Readings.MagneticFieldValid)
			{
				MagneticField = ImuSensor.Readings.MagneticField;
			}

			if (ImuSensor.Readings.FusionPoseValid)
			{
				Pose = ImuSensor.Readings.FusionPose;
			}
		}

		private void PressureReadingsChanged()
		{
			if (PressureSensor.Readings.PressureValid)
			{
				Pressure = PressureSensor.Readings.Pressure;
			}
		}

		private void HumidityReadingsChanged()
		{
			if (HumiditySensor.Readings.TemperatureValid)
			{
				Temperature = HumiditySensor.Readings.Temperature;
			}

			if (HumiditySensor.Readings.HumidityValid)
			{
				Humidity = HumiditySensor.Readings.Humidity;
			}
		}
	}
}