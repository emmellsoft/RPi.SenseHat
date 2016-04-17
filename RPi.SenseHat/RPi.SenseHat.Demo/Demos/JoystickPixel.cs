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
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// Use the joystick to move the pixel around.
	/// </summary>
	public class JoystickPixel : SenseHatDemo
	{
		public JoystickPixel(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		public override void Run()
		{
			// The initial position of the pixel.
			int x = 3;
			int y = 3;

			SenseHat.Display.Clear();

			while (true)
			{
				if (SenseHat.Joystick.Update()) // Has any of the buttons on the joystick changed?
				{
					UpdatePosition(ref x, ref y); // Move the pixel.

					SenseHat.Display.Clear(); // Clear the screen.

					SenseHat.Display.Screen[x, y] = Colors.Yellow; // Draw the pixel.

					SenseHat.Display.Update(); // Update the physical display.

					MainPage?.SetScreenText($"{x}, {y}"); // Update the MainPage (if it's utilized; i.e. not null).
				}

				// Take a short nap.
				Sleep(TimeSpan.FromMilliseconds(2));
			}
		}

		private void UpdatePosition(ref int x, ref int y)
		{
			if (SenseHat.Joystick.LeftKey == KeyState.Pressed)
			{
				if (x > 0)
				{
					x--;
				}
			}
			else if (SenseHat.Joystick.RightKey == KeyState.Pressed)
			{
				if (x < 7)
				{
					x++;
				}
			}

			if (SenseHat.Joystick.UpKey == KeyState.Pressed)
			{
				if (y > 0)
				{
					y--;
				}
			}
			else if (SenseHat.Joystick.DownKey == KeyState.Pressed)
			{
				if (y < 7)
				{
					y++;
				}
			}
		}
	}
}