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
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace RichardsTech.Sensors.Devices.LPS25H
{
	/// <summary>
	/// The LPS25H pressure-sensor
	/// </summary>
	public class LPS25HPressureSensor : PressureSensor
	{
		private readonly byte _i2CAddress;
		private I2cDevice _i2CDevice;

		public LPS25HPressureSensor(byte i2CAddress)
		{
			_i2CAddress = i2CAddress;
		}

		protected override async Task<bool> InitDeviceAsync()
		{
			await ConnectToI2CDevices();

			I2CSupport.Write(_i2CDevice, LPS25HDefines.CTRL_REG_1, 0xc4, "Failed to set LPS25H CTRL_REG_1");

			I2CSupport.Write(_i2CDevice, LPS25HDefines.RES_CONF, 0x05, "Failed to set LPS25H RES_CONF");

			I2CSupport.Write(_i2CDevice, LPS25HDefines.FIFO_CTRL, 0xc0, "Failed to set LPS25H FIFO_CTRL");

			I2CSupport.Write(_i2CDevice, LPS25HDefines.CTRL_REG_2, 0x40, "Failed to set LPS25H CTRL_REG_2");

			return true;
		}

		private async Task ConnectToI2CDevices()
		{
			try
			{
				string aqsFilter = I2cDevice.GetDeviceSelector("I2C1");

				DeviceInformationCollection collection = await DeviceInformation.FindAllAsync(aqsFilter);
				if (collection.Count == 0)
				{
					throw new SensorException("I2C device not found");
				}

				I2cConnectionSettings i2CSettings = new I2cConnectionSettings(_i2CAddress)
				{
					BusSpeed = I2cBusSpeed.FastMode
				};

				_i2CDevice = await I2cDevice.FromIdAsync(collection[0].Id, i2CSettings);
			}
			catch (Exception exception)
			{
				throw new SensorException("Failed to connect to LPS25H", exception);
			}
		}

		/// <summary>
		/// Tries to update the readings.
		/// Returns true if new readings are available, otherwise false.
		/// An exception is thrown if something goes wrong.
		/// </summary>
		public override bool Update()
		{
			byte status = I2CSupport.Read8Bits(_i2CDevice, LPS25HDefines.STATUS_REG, "Failed to read LPS25H status");

			var readings = new SensorReadings
			{
				Timestamp = DateTime.Now
			};

			if ((status & 0x02) == 0x02)
			{
				Int32 rawPressure = (Int32)I2CSupport.Read24Bits(_i2CDevice, LPS25HDefines.PRESS_OUT_XL + 0x80, ByteOrder.LittleEndian, "Failed to read LPS25H pressure");

				readings.Pressure = rawPressure / 4096.0;
				readings.PressureValid = true;
			}

			if ((status & 0x01) == 0x01)
			{
				Int16 rawTemperature = (Int16)I2CSupport.Read16Bits(_i2CDevice, LPS25HDefines.TEMP_OUT_L + 0x80, ByteOrder.LittleEndian, "Failed to read LPS25H temperature");

				readings.Temperature = rawTemperature / 480.0 + 42.5;
				readings.TemperatureValid = true;
			}

			if (readings.PressureValid || readings.TemperatureValid)
			{
				AssignNewReadings(readings);
				return true;
			}

			return false;
		}
	}
}
