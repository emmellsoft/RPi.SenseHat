using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Emmellsoft.IoT.Rpi.SenseHat;
using RPi.SenseHat.Demo.Demos;

namespace RPi.SenseHat.Demo
{
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			InitializeComponent();

			Task.Run(() => MainLoop());
		}

		private void MainLoop()
		{
			var senseHat = SenseHatFactory.Singleton.Create().Result;

			var waitEvent = new ManualResetEventSlim(false);

			SenseHatDemo gravityBlob = new GravityBlob(senseHat);

			while (true)
			{
				waitEvent.Wait(50);

				gravityBlob.Update();
			}
		}
	}
}
