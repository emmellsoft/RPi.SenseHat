using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.RPi.SenseHat.Demo;

namespace RPi.SenseHat.Demo.Core
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CoreDeviceFactory.Init();

            ISenseHat senseHat = SenseHatFactory.GetSenseHat();

            DemoSelector
                .GetDemo(senseHat, text => { })
                .Run();
        }
    }
}
