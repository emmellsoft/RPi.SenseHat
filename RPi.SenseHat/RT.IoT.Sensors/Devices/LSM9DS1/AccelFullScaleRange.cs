namespace RichardsTech.Sensors.Devices.LSM9DS1
{
	public enum AccelFullScaleRange
	{
		Range2g = 0,
		Range16g = 1, // TODO: Is the order here correct!?  ("LSM9DS1_ACCEL_FSR_...")
		Range4g = 2,
		Range8g = 3,
	}
}