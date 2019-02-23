using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.RPi.SenseHat.Demo;
using System.Threading.Tasks;

namespace RPi.SenseHat.Demo.Core
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            CoreDeviceFactory.Init();

            ISenseHat senseHat = await SenseHatFactory.GetSenseHat().ConfigureAwait(false);

            DemoSelector
                .GetDemo(senseHat, text => { })
                .Run();
        }
    }
}
