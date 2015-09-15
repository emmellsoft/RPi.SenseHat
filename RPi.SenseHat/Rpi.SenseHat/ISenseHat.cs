using System;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public interface ISenseHat : IDisposable
	{
		byte FirmwareVersion
		{ get; }

		ISenseHatDisplay Display
		{ get; }

		ISenseHatJoystick Joystick
		{ get; }

		ISenseHatSensors Sensors
		{ get; }
	}
}