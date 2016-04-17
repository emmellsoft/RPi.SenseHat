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
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// Is it only me or does it show some unusual high temperature? :-S
	/// </summary>
	public class WriteTemperature : SenseHatDemo
	{
		public WriteTemperature(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		private enum TemperatureUnit
		{
			Celcius,
			Fahrenheit,
			Kelvin
		}

		public override void Run()
		{
			var tinyFont = new TinyFont();

			ISenseHatDisplay display = SenseHat.Display;

			TemperatureUnit unit = TemperatureUnit.Celcius; // The wanted temperature unit.

			string unitText = GetUnitText(unit); // Get the unit as a string.

			while (true)
			{
				SenseHat.Sensors.HumiditySensor.Update();

				if (SenseHat.Sensors.Temperature.HasValue)
				{
					double temperatureValue = ConvertTemperatureValue(unit, SenseHat.Sensors.Temperature.Value);

					int temperature = (int)Math.Round(temperatureValue);
					string text = temperature.ToString();

					if (text.Length > 2)
					{
						// Too long to fit the display!
						text = "**";
					}

					display.Clear();
					tinyFont.Write(display, text, Colors.White);
					display.Update();

					MainPage?.SetScreenText($"{temperatureValue:0.0} {unitText}"); // Update the MainPage (if it's utilized; i.e. not null).

					// Sleep quite some time; the temperature usually change quite slowly...
					Sleep(TimeSpan.FromSeconds(5));
				}
				else
				{
					// Rapid update until there is a temperature reading.
					Sleep(TimeSpan.FromSeconds(0.5));
				}
			}
		}

		private static double ConvertTemperatureValue(TemperatureUnit unit, double temperatureInCelcius)
		{
			switch (unit)
			{
				case TemperatureUnit.Celcius:
					return temperatureInCelcius;

				case TemperatureUnit.Fahrenheit:
					return temperatureInCelcius * 9 / 5 + 32;

				case TemperatureUnit.Kelvin:
					return temperatureInCelcius + 273.15;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private static string GetUnitText(TemperatureUnit unit)
		{
			switch (unit)
			{
				case TemperatureUnit.Celcius:
					return "\u00B0C"; // Where "\u00B0" is the degree-symbol.

				case TemperatureUnit.Fahrenheit:
					return "\u00B0F"; // Where "\u00B0" is the degree-symbol.

				case TemperatureUnit.Kelvin:
					return "K";

				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}