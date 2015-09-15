namespace RichardsTech.Sensors.Devices.LSM9DS1
{
	public enum GyroFullScaleRange
	{
		Range250 = 0, // TODO: Shouldn't this be 245?  ("LSM9DS1_GYRO_FSR_...")
		Range500 = 1,
		Range2000 = 2 // TODO: Shouldn't this be 3!? (see datasheet)  ("LSM9DS1_GYRO_FSR_...")
	}
}