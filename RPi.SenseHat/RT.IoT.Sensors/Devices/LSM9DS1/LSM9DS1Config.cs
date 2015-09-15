namespace RichardsTech.Sensors.Devices.LSM9DS1
{
	/// <summary>
	/// Configuration of the LSM9DS1 IMU-sensor.
	/// </summary>
	public class LSM9DS1Config
	{
		public LSM9DS1Config()
		{
			GyroSampleRate = GyroSampleRate.Freq119Hz;
			GyroBandwidthCode = GyroBandwidthCode.BandwidthCode1;
			GyroHighPassFilterCode = GyroHighPassFilterCode.FilterCode4;
			GyroFullScaleRange = GyroFullScaleRange.Range500;

			AccelSampleRate = AccelSampleRate.Freq119Hz;
			AccelFullScaleRange = AccelFullScaleRange.Range8g;
			AccelLowPassFilter = AccelLowPassFilter.Freq50Hz;

			CompassSampleRate = CompassSampleRate.Freq20Hz;
			MagneticFullScaleRange = MagneticFullScaleRange.Range4Gauss;
		}

		/// <summary>
		/// The gyro sample rate
		/// </summary>
		public GyroSampleRate GyroSampleRate
		{ get; private set; }

		/// <summary>
		/// The gyro bandwidth code
		/// </summary>
		public GyroBandwidthCode GyroBandwidthCode
		{ get; private set; }

		/// <summary>
		/// The gyro high pass filter cutoff code
		/// </summary>
		public GyroHighPassFilterCode GyroHighPassFilterCode
		{ get; private set; }

		/// <summary>
		/// The gyro full scale range
		/// </summary>
		public GyroFullScaleRange GyroFullScaleRange
		{ get; private set; }

		/// <summary>
		/// The accel sample rate
		/// </summary>
		public AccelSampleRate AccelSampleRate
		{ get; private set; }

		/// <summary>
		/// The accel full scale range
		/// </summary>
		public AccelFullScaleRange AccelFullScaleRange
		{ get; private set; }

		/// <summary>
		/// The accel low pass filter
		/// </summary>
		public AccelLowPassFilter AccelLowPassFilter
		{ get; private set; }

		/// <summary>
		/// The compass sample rate
		/// </summary>
		public CompassSampleRate CompassSampleRate
		{ get; private set; }

		/// <summary>
		/// The compass full scale range
		/// </summary>
		public MagneticFullScaleRange MagneticFullScaleRange
		{ get; private set; }
	}
}