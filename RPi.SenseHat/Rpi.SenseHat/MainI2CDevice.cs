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
using Windows.Devices.I2c;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class MainI2CDevice : IDisposable
	{
		private readonly I2cDevice _device;

		public MainI2CDevice(I2cDevice device)
		{
			_device = device;
		}

		public void Dispose()
		{
			_device.Dispose();
		}

		internal byte ReadByte(byte address)
		{
			byte[] buffer = { address };
			byte[] value = new byte[1];

			_device.WriteRead(buffer, value);

			return value[0];
		}

		internal byte[] ReadBytes(byte address, int length)
		{
			byte[] values = new byte[length];
			byte[] buffer = new byte[1];
			buffer[0] = address;

			_device.WriteRead(buffer, values);

			return values;
		}

		internal void WriteBytes(byte address, byte[] values)
		{
			byte[] buffer = new byte[1 + values.Length];
			buffer[0] = address;
			Array.Copy(values, 0, buffer, 1, values.Length);

			_device.Write(buffer);
		}
	}
}