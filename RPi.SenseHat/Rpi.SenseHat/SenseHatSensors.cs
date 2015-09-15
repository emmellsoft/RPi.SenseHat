using RichardsTech.Sensors;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHatSensors : ISenseHatSensors
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

		public Vector3? Gyro
		{ get; private set; }

		public Vector3? Acceleration
		{ get; private set; }

		public Vector3? MagneticField
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