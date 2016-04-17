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
using Windows.Foundation;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// Note! You must calibrate the magnetic sensor by moving the Raspberry Pi device around in an 'eight' figure a few seconds at startup!
	/// </summary>
	public class Compass : SenseHatDemo
	{
		public Compass(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		public override void Run()
		{
			SenseHat.Display.Clear();
			SenseHat.Display.Update();

			const double halfCircle = Math.PI;
			const double fullCircle = Math.PI * 2;

			Color northColor = Colors.Red;
			Color southColor = Colors.White;
			Color centerColor = Colors.DarkBlue;

			TimeSpan mainPageUpdateRate = TimeSpan.FromSeconds(0.5);
			DateTime nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);

			while (true)
			{
				SenseHat.Sensors.ImuSensor.Update();

				if (SenseHat.Sensors.Pose.HasValue)
				{
					double northAngle = SenseHat.Sensors.Pose.Value.Z;
					if (northAngle < 0)
					{
						northAngle += fullCircle;
					}

					northAngle = fullCircle - northAngle;
					double southAngle = northAngle + halfCircle;

					Point northPoint = GetPixelCoordinate(northAngle);
					Point southPoint = GetPixelCoordinate(southAngle);

					SenseHat.Display.Clear();
					SenseHat.Display.Screen[(int)northPoint.X, (int)northPoint.Y] = northColor;
					SenseHat.Display.Screen[(int)southPoint.X, (int)southPoint.Y] = southColor;
					SenseHat.Display.Screen[3, 3] = centerColor;
					SenseHat.Display.Screen[4, 3] = centerColor;
					SenseHat.Display.Screen[3, 4] = centerColor;
					SenseHat.Display.Screen[4, 4] = centerColor;
					SenseHat.Display.Update();

					if ((MainPage != null) && nextMainPageUpdate <= DateTime.Now)
					{
						MainPage.SetScreenText($"{northAngle / fullCircle * 360:0}");
						nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);
					}
				}

				Sleep(TimeSpan.FromMilliseconds(2));
			}
		}

		private static Point GetPixelCoordinate(double angle)
		{
			return new Point(
				Math.Round(Math.Cos(angle) * 3.5 + 3.5),
				Math.Round(Math.Sin(angle) * 3.5 + 3.5));
		}
	}
}