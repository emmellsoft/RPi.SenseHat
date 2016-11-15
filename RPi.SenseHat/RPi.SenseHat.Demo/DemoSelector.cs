////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Demo
//
//  Copyright (c) 2016, Mattias Larsson
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

using System;
using Emmellsoft.IoT.Rpi.SenseHat;
using RPi.SenseHat.Demo.Demos;

namespace RPi.SenseHat.Demo
{
	public static class DemoSelector
	{
		private static bool AlsoUseHdmiOutput = false; // Set this to true/false whether you have a display connected to the HDMI port of the Raspberry Pi!

		public static SenseHatDemo GetDemo(ISenseHat senseHat, Action<string> setScreenText)
		{
			if (!AlsoUseHdmiOutput)
			{
				// If you don't utilize the HDMI output, set the setScreenText parameter to null.
				setScreenText = null;
			}

			//return new DiscoLights(senseHat); // Click on the joystick to change drawing mode!

			//return new JoystickPixel(senseHat, setScreenText); // Use the joystick to move the pixel around.

			//return new WriteTemperature(senseHat, setScreenText); // Is it only me or does it show some unusual high temperature? :-S

			//return new GravityBlob(senseHat, setScreenText); // The green blob is drawn to the center of the earth! If you hold it upside down it gets angry and turns red. :-O

			//return new Compass(senseHat, setScreenText); // Note! You must calibrate the magnetic sensor by moving the Raspberry Pi device around in an 'eight' figure a few seconds at startup!

			//return new SingleColorScrollText(senseHat, "Hello Raspberry Pi 3 Sense HAT!"); // Click on the joystick to change drawing mode!

			//return new MultiColorScrollText(senseHat, "Hello Raspberry Pi 3 Sense HAT!");

			//return new SpriteAnimation(senseHat); // Use the joystick to move Mario. The middle button switches orientation and flipping of the drawing.

			//return new GammaTest(senseHat); // Tries out different gamma settings for the LED display. Use the joystick to play around.

			return new ReadAllSensors(senseHat, setScreenText); // Shows an example of how to read all the different sensors.
		}
	}
}