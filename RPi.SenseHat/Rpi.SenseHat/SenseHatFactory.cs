using System;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using RichardsTech.Sensors;
using RichardsTech.Sensors.Devices.HTS221;
using RichardsTech.Sensors.Devices.LPS25H;
using RichardsTech.Sensors.Devices.LSM9DS1;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public sealed class SenseHatFactory
	{
		public static readonly SenseHatFactory Singleton = new SenseHatFactory();

		private const byte DeviceAddress = 0x46;

		private SenseHat _senseHat;

		private SenseHatFactory()
		{
		}

		public async Task<ISenseHat> Create()
		{
			if (_senseHat != null)
			{
				return _senseHat;
			}

			I2cDevice device = await CreateI2CDevice();

			ImuSensor imuSensor = await CreateImuSensor();

			PressureSensor pressureSensor = await CreatePressureSensor();

			HumiditySensor humiditySensor = await CreateHumiditySensor();

			_senseHat = new SenseHat(device, imuSensor, pressureSensor, humiditySensor);

			return _senseHat;
		}

		private static async Task<I2cDevice> CreateI2CDevice()
		{
			string aqsFilter = I2cDevice.GetDeviceSelector();

			DeviceInformationCollection collection = await DeviceInformation.FindAllAsync(aqsFilter);

			I2cConnectionSettings settings = new I2cConnectionSettings(DeviceAddress)
			{
				BusSpeed = I2cBusSpeed.StandardMode,
				SharingMode = I2cSharingMode.Exclusive
			};

			return await I2cDevice.FromIdAsync(collection[0].Id, settings);
		}

		private static async Task<ImuSensor> CreateImuSensor()
		{
			var lsm9Ds1Config = new LSM9DS1Config();
			var imuSensor = new LSM9DS1ImuSensor(LSM9DS1Defines.ADDRESS0, LSM9DS1Defines.MAG_ADDRESS0, lsm9Ds1Config);
			await imuSensor.InitAsync();
			return imuSensor;
		}

		private static async Task<PressureSensor> CreatePressureSensor()
		{
			var pressureSensor = new LPS25HPressureSensor(LPS25HDefines.ADDRESS0);
			await pressureSensor.InitAsync();
			return pressureSensor;
		}

		private static async Task<HumiditySensor> CreateHumiditySensor()
		{
			var humiditySensor = new HTS221HumiditySensor(HTS221Defines.ADDRESS);
			await humiditySensor.InitAsync();
			return humiditySensor;
		}
	}
}