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
using RichardsTech.Sensors;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// The green blob is drawn to the center of the earth! If you hold it upside down it gets angry and turns red. :-O
	/// </summary>
	public sealed class GravityBlob : SenseHatDemo
	{
		public GravityBlob(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		public override void Run()
		{
			TimeSpan mainPageUpdateRate = TimeSpan.FromSeconds(0.5);
			DateTime nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);

			while (true)
			{
				Sleep(TimeSpan.FromMilliseconds(50));

				if (!SenseHat.Sensors.ImuSensor.Update())
				{
					continue;
				}

				if (!SenseHat.Sensors.Acceleration.HasValue)
				{
					continue;
				}

				Color[,] colors = CreateGravityBlobScreen(SenseHat.Sensors.Acceleration.Value);

				SenseHat.Display.CopyColorsToScreen(colors);

				SenseHat.Display.Update();

				if ((MainPage != null) && nextMainPageUpdate <= DateTime.Now)
				{
					MainPage.SetScreenText($"{SenseHat.Sensors.Acceleration.Value.X:0.00}, {SenseHat.Sensors.Acceleration.Value.Y:0.00}, {SenseHat.Sensors.Acceleration.Value.Z:0.00}");
					nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);
				}
			}
		}

		private static Color[,] CreateGravityBlobScreen(Vector3 vector)
		{
			double x0 = (vector.X + 1) * 5.5 - 2;
			double y0 = (vector.Y + 1) * 5.5 - 2;

			double distScale = 4;

			var colors = new Color[8, 8];

			bool isUpsideDown = vector.Z < 0;

			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					double dx = x0 - x;
					double dy = y0 - y;

					double dist = Math.Sqrt(dx * dx + dy * dy) / distScale;
					if (dist > 1)
					{
						dist = 1;
					}

					int colorIntensity = (int)Math.Round(255 * (1 - dist));
					if (colorIntensity > 255)
					{
						colorIntensity = 255;
					}

					colors[x, y] = isUpsideDown
						? Color.FromArgb(255, (byte)colorIntensity, 0, 0)
						: Color.FromArgb(255, 0, (byte)colorIntensity, 0);
				}
			}

			return colors;
		}
	}
}