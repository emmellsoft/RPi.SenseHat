using System.Threading.Tasks;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Abstractions;

namespace RTIMULibCS.Unosquare
{
    public class UnosquareI2CDeviceFactory : I2CDeviceFactory
    {
        public static void Init()
        {
            Init(new UnosquareI2CDeviceFactory());
        }

        public override Task<II2CDevice> Create(byte deviceId)
        {
            return Task.FromResult(Pi.I2C.AddDevice(deviceId));
        }
    }
}
