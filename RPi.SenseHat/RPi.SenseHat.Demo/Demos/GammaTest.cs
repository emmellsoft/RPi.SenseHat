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
using System.Linq;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// Tries out different gamma settings for the LED display. Use the joystick to play around.
	/// </summary>
	public class GammaTest : SenseHatDemo
	{
		[Flags]
		private enum ColorComponents
		{
			Red = 1,
			Green = 2,
			Blue = 4,

			All = Red | Green | Blue
		}

		public GammaTest(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		public override void Run()
		{
			SenseHat.Display.Clear();

			double redGamma = 1.8;
			double greenGamma = 2.0;
			double blueGamma = 1.8;

			ColorComponents colorComponents = ColorComponents.All;

			while (true)
			{
				if (SenseHat.Joystick.Update())
				{
					if (SenseHat.Joystick.LeftKey == KeyState.Pressed)
					{
						GetPrevColorComponent(ref colorComponents);
						GenerateTestPattern(colorComponents);
					}
					else if (SenseHat.Joystick.RightKey == KeyState.Pressed)
					{
						GetNextColorComponent(ref colorComponents);
						GenerateTestPattern(colorComponents);
					}

					if (SenseHat.Joystick.UpKey == KeyState.Pressed)
					{
						if (colorComponents.HasFlag(ColorComponents.Red))
						{
							StepUpGamma(ref redGamma);
							SenseHat.Display.RedGamma = redGamma;
						}

						if (colorComponents.HasFlag(ColorComponents.Green))
						{
							StepUpGamma(ref greenGamma);
							SenseHat.Display.GreenGamma = greenGamma;
						}

						if (colorComponents.HasFlag(ColorComponents.Blue))
						{
							StepUpGamma(ref blueGamma);
							SenseHat.Display.BlueGamma = blueGamma;
						}
					}
					else if (SenseHat.Joystick.DownKey == KeyState.Pressed)
					{
						if (colorComponents.HasFlag(ColorComponents.Red))
						{
							StepDownGamma(ref redGamma);
							SenseHat.Display.RedGamma = redGamma;
						}

						if (colorComponents.HasFlag(ColorComponents.Green))
						{
							StepDownGamma(ref greenGamma);
							SenseHat.Display.GreenGamma = greenGamma;
						}

						if (colorComponents.HasFlag(ColorComponents.Blue))
						{
							StepDownGamma(ref blueGamma);
							SenseHat.Display.BlueGamma = blueGamma;
						}
					}

					SenseHat.Display.Update(); // Update the physical display.
				}

				// Take a short nap.
				Sleep(TimeSpan.FromMilliseconds(2));
			}
		}

		private static void GetNextColorComponent(ref ColorComponents colorComponents)
		{
			switch (colorComponents)
			{
				case ColorComponents.All:
					colorComponents = ColorComponents.Red;
					break;
				case ColorComponents.Red:
					colorComponents = ColorComponents.Green;
					break;
				case ColorComponents.Green:
					colorComponents = ColorComponents.Blue;
					break;
				case ColorComponents.Blue:
					colorComponents = ColorComponents.All;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(colorComponents), colorComponents, null);
			}
		}

		private static void GetPrevColorComponent(ref ColorComponents colorComponents)
		{
			switch (colorComponents)
			{
				case ColorComponents.All:
					colorComponents = ColorComponents.Blue;
					break;
				case ColorComponents.Red:
					colorComponents = ColorComponents.All;
					break;
				case ColorComponents.Green:
					colorComponents = ColorComponents.Red;
					break;
				case ColorComponents.Blue:
					colorComponents = ColorComponents.Green;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(colorComponents), colorComponents, null);
			}
		}

		private static void StepUpGamma(ref double gamma)
		{
			gamma += 0.1;
			if (gamma > 5)
			{
				gamma = 5;
			}
		}

		private static void StepDownGamma(ref double gamma)
		{
			gamma -= 0.1;
			if (gamma < 0)
			{
				gamma = 0;
			}
		}

		private void GenerateTestPattern(ColorComponents colorComponents)
		{
			bool showRed = colorComponents.HasFlag(ColorComponents.Red);
			bool showGreen = colorComponents.HasFlag(ColorComponents.Green);
			bool showBlue = colorComponents.HasFlag(ColorComponents.Blue);

			byte[] intensities = Enumerable
				.Range(0, 32) // 5-bit colors has a intensity component of 0-31
				.Select(x => (byte)(x << 3)) // Scale up to 8 bits.
				.ToArray();

			Func<byte, Color> getColor = intensity =>
				Color.FromArgb(
					255,
					showRed ? intensity : (byte)0,
					showGreen ? intensity : (byte)0,
					showBlue ? intensity : (byte)0);

			for (int x = 0; x < 8; x++)
			{
				SenseHat.Display.Screen[x, 0] = getColor(intensities[x]);
				SenseHat.Display.Screen[x, 1] = getColor(intensities[15 - x]);
				SenseHat.Display.Screen[x, 2] = getColor(intensities[16 + x]);
				SenseHat.Display.Screen[x, 3] = getColor(intensities[31 - x]);
				SenseHat.Display.Screen[x, 4] = getColor(intensities[31 - x]);
				SenseHat.Display.Screen[x, 5] = getColor(intensities[16 + x]);
				SenseHat.Display.Screen[x, 6] = getColor(intensities[15 - x]);
				SenseHat.Display.Screen[x, 7] = getColor(intensities[x]);
			}
		}
	}
}