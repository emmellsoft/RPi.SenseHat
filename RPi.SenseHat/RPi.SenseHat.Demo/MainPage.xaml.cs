////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Demo
//
//  Copyright (c) 2015, Mattias Larsson
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of 
//  this software and associated documentation files (the "Software"), to deal in 
//  the Software without restriction, including without limitation the rights to use, 
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
//  Software, and to permit persons to whom the Software is furnished to do so, 
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all 
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

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
			ISenseHat senseHat = SenseHatFactory.Singleton.Create().Result;

			//--------------------------------------------------------------------
			// Activate the demo you want to run down here below!
			// NOTE that they will each run forever, so only the first demo will run!
			//--------------------------------------------------------------------

			new DiscoLights(senseHat).Run();

			//new JoystickPixel(senseHat).Run();

			//new WriteTemperature(senseHat).Run(); // Is it only me or does it show some unusual high temperature? :-S

			//new GravityBlob(senseHat).Run();

			//new Compass(senseHat).Run(); // Note! You must calibrate the magnetic sensor by moving the Raspberry Pi device around in an 'eight' figure a few seconds at startup!

			//new BwScrollText(senseHat, "Hello Raspberry Pi Sense HAT!").Run();

			//new GammaTest(senseHat).Run();
		}
	}
}
