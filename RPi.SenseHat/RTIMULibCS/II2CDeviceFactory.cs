using System.Threading.Tasks;

namespace RTIMULibCS
{
    public abstract class I2CDeviceFactory
    {
        public static I2CDeviceFactory Singleton { get; private set; }

        protected static void Init(I2CDeviceFactory singleton)
        {
            Singleton = singleton;
        }

        public abstract Task<II2C> Create(byte deviceAddress);
    }
}
