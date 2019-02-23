using RTIMULibCS;
using System.Device.I2c;
using System.Device.I2c.Drivers;
using System.Threading.Tasks;

namespace RPi.SenseHat.Demo.Core
{
    public class CoreDeviceFactory : I2CDeviceFactory
    {
        public static void Init()
        {
            Init(new CoreDeviceFactory());
        }

        public override Task<II2C> Create(byte deviceAddress)
        {
            I2cDevice device = new Windows10I2cDevice(new I2cConnectionSettings(1, deviceAddress));
            return Task.FromResult<II2C>(new UwpI2C(device));
        }

        private class UwpI2C : II2C
        {
            private readonly I2cDevice _i2CDevice;

            public UwpI2C(I2cDevice i2CDevice)
            {
                _i2CDevice = i2CDevice;
            }

            public byte[] Read(int length)
            {
                var buffer = new byte[length];
                _i2CDevice.Read(buffer);
                return buffer;
            }

            public void Write(byte[] data)
            {
                _i2CDevice.Write(data);
            }
        }
    }
}
