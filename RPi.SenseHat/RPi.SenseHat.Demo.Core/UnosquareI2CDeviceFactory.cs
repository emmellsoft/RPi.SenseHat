////using System.Threading.Tasks;
////using Unosquare.RaspberryIO;
////using Unosquare.RaspberryIO.Abstractions;

////namespace RTIMULibCS.Unosquare
////{
////    public class UnosquareI2CDeviceFactory : I2CDeviceFactory
////    {
////        public static void Init()
////        {
////            Init(new UnosquareI2CDeviceFactory());
////        }

////        public override Task<II2C> Create(byte deviceId)
////        {
////            return Task.FromResult<II2C>(new UnosquareI2C(Pi.I2C.AddDevice(deviceId)));
////        }

////        private class UnosquareI2C : II2C
////        {
////            private readonly II2CDevice _i2CDevice;

////            public UnosquareI2C(II2CDevice i2CDevice)
////            {
////                _i2CDevice = i2CDevice;
////            }

////            public byte[] Read(int length)
////            {
////                return _i2CDevice.Read(length);
////            }

////            public void Write(byte[] data)
////            {
////                _i2CDevice.Write(data);
////            }
////        }
////    }
////}
