using RichardsTech.Sensors;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public interface ISenseHatSensors
	{
		Vector3? Gyro
		{ get; }

		Vector3? Acceleration
		{ get; }

		Vector3? MagneticField
		{ get; }

		double? Pressure
		{ get; }

		double? Temperature
		{ get; }

		double? Humidity
		{ get; }

		ImuSensor ImuSensor
		{ get; }

		PressureSensor PressureSensor
		{ get; }

		HumiditySensor HumiditySensor
		{ get; }
	}
}