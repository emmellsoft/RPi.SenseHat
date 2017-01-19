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
	internal sealed class SenseHat : ISenseHat
	{
		private readonly MainI2CDevice _mainI2CDevice;
		private bool _isDisposed;

		public SenseHat(
			MainI2CDevice mainI2CDevice,
			ImuSensor imuSensor,
			PressureSensor pressureSensor,
			HumiditySensor humiditySensor)
		{
			_mainI2CDevice = mainI2CDevice;

			Display = new SenseHatDisplay(_mainI2CDevice);
			Joystick = new SenseHatJoystick(_mainI2CDevice);
			Sensors = new SenseHatSensors(imuSensor, pressureSensor, humiditySensor);
		}

		public void Dispose()
		{
			if (_isDisposed)
			{
				return;
			}

			_mainI2CDevice.Dispose();
			((IDisposable)Sensors).Dispose();
			_isDisposed = true;
		}

		public byte FirmwareVersion => _mainI2CDevice.ReadByte(0xf1);

		public ISenseHatDisplay Display
		{ get; }

		public ISenseHatJoystick Joystick
		{ get; }

		public ISenseHatSensors Sensors
		{ get; }
	}
}