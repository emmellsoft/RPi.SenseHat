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

namespace RTIMULibCS.Devices.LPS25H
{
    /// <summary>
    /// The LPS25H pressure-sensor
    /// </summary>
    public class LPS25HPressureSensor : PressureSensor
    {
        private readonly byte _i2CAddress;
        private II2C _i2CDevice;

        private bool _pressureValid;
        private double _pressure;
        private bool _temperatureValid;
        private double _temperature;

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
                _i2CDevice = await I2CDeviceFactory.Singleton.Create(_i2CAddress);
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
            bool newReadings = false;

            byte status = I2CSupport.Read8Bits(_i2CDevice, LPS25HDefines.STATUS_REG, "Failed to read LPS25H status");

            var readings = new SensorReadings
            {
                Timestamp = DateTime.Now
            };

            if ((status & 0x02) == 0x02)
            {
                Int32 rawPressure = (Int32)I2CSupport.Read24Bits(_i2CDevice, LPS25HDefines.PRESS_OUT_XL + 0x80, ByteOrder.LittleEndian, "Failed to read LPS25H pressure");

                _pressure = rawPressure / 4096.0;
                _pressureValid = true;
                newReadings = true;
            }

            if ((status & 0x01) == 0x01)
            {
                Int16 rawTemperature = (Int16)I2CSupport.Read16Bits(_i2CDevice, LPS25HDefines.TEMP_OUT_L + 0x80, ByteOrder.LittleEndian, "Failed to read LPS25H temperature");

                _temperature = rawTemperature / 480.0 + 42.5;
                _temperatureValid = true;
                newReadings = true;
            }

            if (newReadings)
            {
                readings.Pressure = _pressure;
                readings.PressureValid = _pressureValid;
                readings.Temperature = _temperature;
                readings.TemperatureValid = _temperatureValid;
                AssignNewReadings(readings);
                return true;
            }

            return false;
        }
    }
}
