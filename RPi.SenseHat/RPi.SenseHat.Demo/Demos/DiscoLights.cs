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
	/// Click on the joystick to change drawing mode!
	/// </summary>
	public class DiscoLights : SenseHatDemo
	{
		private readonly static Random Random = new Random();
		private ColorMode _currentMode;

		private enum ColorMode
		{
			SoftRandom,
			HardRandom,
			Sparkling,
			Blocks,
			Unicolor
		}

		public DiscoLights(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		public override void Run()
		{
			while (true)
			{
				FillDisplay();

				SenseHat.Display.Update();

				// Should the drawing mode change?
				if (SenseHat.Joystick.Update() && (SenseHat.Joystick.EnterKey == KeyState.Pressing))
				{
					// The middle button is just pressed.
					SwitchToNextColorMode();
				}

				Sleep(TimeSpan.FromMilliseconds(50));
			}
		}

		private void SwitchToNextColorMode()
		{
			_currentMode++;

			if (_currentMode > ColorMode.Unicolor)
			{
				_currentMode = ColorMode.SoftRandom;
			}

			SenseHat.Display.Clear();
		}

		private void FillDisplay()
		{
			switch (_currentMode)
			{
				case ColorMode.SoftRandom:
					FillDisplaySoftRandom();
					break;

				case ColorMode.HardRandom:
					FillDisplayHardRandom();
					break;

				case ColorMode.Sparkling:
					FillDisplaySparkling();
					break;

				case ColorMode.Blocks:
					FillDisplayBlocks();
					break;

				case ColorMode.Unicolor:
					FillDisplayUnicolor();
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void FillDisplaySoftRandom()
		{
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					Color pixel = Color.FromArgb(
						255,
						(byte)Random.Next(256),
						(byte)Random.Next(256),
						(byte)Random.Next(256));

					SenseHat.Display.Screen[x, y] = pixel;
				}
			}
		}

		private void FillDisplayHardRandom()
		{
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					Color pixel = Color.FromArgb(
						255,
						(byte)(Random.Next(2) * 255),
						(byte)(Random.Next(2) * 255),
						(byte)(Random.Next(2) * 255));

					SenseHat.Display.Screen[x, y] = pixel;
				}
			}
		}

		private void FillDisplaySparkling()
		{
			const double probabilityForNewSparkle = 0.99; // 1=always new sparkle, 0=never.
			const double oldSparkleFadeRate = 0.75;       // The decrease in percentage of the intensity from one frame to another (0.5 = 50 % decrease).

			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					bool sparkle = Random.NextDouble() >= probabilityForNewSparkle;

					Color pixel;
					if (sparkle)
					{
						// This pixel should start a new sparkle.
						pixel = Colors.White;
					}
					else
					{
						// Get the last color of this pixel.
						byte lastIntensity = SenseHat.Display.Screen[x, y].R;
						if (lastIntensity <= 10)
						{
							// Quite dark -- be pitch black.
							pixel = Colors.Black;
						}
						else
						{
							// Turn down the intensity.
							byte newIntensity = (byte)Math.Round(lastIntensity * oldSparkleFadeRate);

							pixel = Color.FromArgb(255, newIntensity, newIntensity, newIntensity);
						}
					}

					SenseHat.Display.Screen[x, y] = pixel;
				}
			}
		}

		private void FillDisplayBlocks()
		{
			for (int y = 0; y < 8; y += 2)
			{
				for (int x = 0; x < 8; x += 2)
				{
					Color pixel = Color.FromArgb(
						255,
						(byte)(Random.Next(2) * 255),
						(byte)(Random.Next(2) * 255),
						(byte)(Random.Next(2) * 255));

					SenseHat.Display.Screen[x, y] = pixel;
					SenseHat.Display.Screen[x + 1, y] = pixel;
					SenseHat.Display.Screen[x, y + 1] = pixel;
					SenseHat.Display.Screen[x + 1, y + 1] = pixel;
				}
			}
		}

		private void FillDisplayUnicolor()
		{
			SenseHat.Display.Fill(Color.FromArgb(
				255,
				(byte)(Random.Next(2) * 255),
				(byte)(Random.Next(2) * 255),
				(byte)(Random.Next(2) * 255)));
		}
	}
}