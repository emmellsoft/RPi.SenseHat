using Emmellsoft.IoT.RPi.SenseHat;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Emmellsoft.IoT.RPi.SenseHat.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();

            UwpI2CDeviceFactory.Init();

            Task.Factory.StartNew(() =>
            {
                ISenseHat senseHat = SenseHatFactory.GetSenseHat();
                SenseHatDemo demo = DemoSelector.GetDemo(senseHat, SetScreenText);
                demo.Run();
            });
        }

        private async void SetScreenText(string text)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal,
                () =>
                {
                    ScreenText.Text = text;

                    // Feel free to add more UI stuff here! :-)
                });
        }
    }
}
