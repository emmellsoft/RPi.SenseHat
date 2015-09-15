using System;
using Windows.Devices.I2c;
using RichardsTech.Sensors;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHat : ISenseHat
	{
		private readonly I2cDevice _device;
		private bool _isDisposed;

		public SenseHat(
			I2cDevice device,
			ImuSensor imuSensor,
			PressureSensor pressureSensor,
			HumiditySensor humiditySensor)
		{
			_device = device;

			Display = new SenseHatDisplay(this);
			Joystick = new SenseHatJoystick(this);
			Sensors = new SenseHatSensors(imuSensor, pressureSensor, humiditySensor);
		}

		public void Dispose()
		{
			if (_isDisposed)
			{
				return;
			}

			_device.Dispose();
			_isDisposed = true;
		}

		public byte FirmwareVersion => ReadByte(0xf1);

		public ISenseHatDisplay Display
		{ get; }

		public ISenseHatJoystick Joystick
		{ get; }

		public ISenseHatSensors Sensors
		{ get; }

		internal byte ReadByte(byte regAddr)
		{
			byte[] buffer = { regAddr };
			byte[] value = new byte[1];

			_device.WriteRead(buffer, value);

			return value[0];
		}

		internal byte[] ReadBytes(byte regAddr, int length)
		{
			byte[] values = new byte[length];
			byte[] buffer = new byte[1];
			buffer[0] = regAddr;

			_device.WriteRead(buffer, values);

			return values;
		}

		internal void WriteByte(byte regAddr, byte data)
		{
			byte[] buffer = new byte[2];
			buffer[0] = regAddr;
			buffer[1] = data;

			_device.Write(buffer);
		}

		internal void WriteBytes(byte regAddr, byte[] values)
		{
			byte[] buffer = new byte[1 + values.Length];
			buffer[0] = regAddr;
			Array.Copy(values, 0, buffer, 1, values.Length);

			_device.Write(buffer);
		}
	}
}