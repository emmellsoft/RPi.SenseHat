////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Demo
//
//  Copyright (c) 2017, Mattias Larsson
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
using System.Diagnostics;
using System.Text;
using Emmellsoft.IoT.Rpi.SenseHat;

namespace RPi.SenseHat.Demo.Demos
{
	public sealed class ReadAllSensors : SenseHatDemo
	{
		public ReadAllSensors(ISenseHat senseHat, Action<string> setScreenText)
			: base(senseHat, setScreenText)
		{
		}

		public override void Run()
		{
			TimeSpan mainPageUpdateRate = TimeSpan.FromSeconds(0.5);
			DateTime nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);

			var stringBuilder = new StringBuilder();

			while (true)
			{
				Sleep(TimeSpan.FromMilliseconds(50));

				SenseHat.Sensors.ImuSensor.Update();      // Try get a new read-out for the Gyro, Acceleration, MagneticField and Pose.
				SenseHat.Sensors.PressureSensor.Update(); // Try get a new read-out for the Pressure.
				SenseHat.Sensors.HumiditySensor.Update(); // Try get a new read-out for the Temperature and Humidity.

				// Build up the string
				stringBuilder.Clear();
				stringBuilder.AppendLine($"Gyro: {SenseHat.Sensors.Gyro?.ToString(false) ?? "N/A"}");          // From the ImuSensor.
				stringBuilder.AppendLine($"Accel: {SenseHat.Sensors.Acceleration?.ToString(false) ?? "N/A"}"); // From the ImuSensor.
				stringBuilder.AppendLine($"Mag: {SenseHat.Sensors.MagneticField?.ToString(false) ?? "N/A"}");  // From the ImuSensor.
				stringBuilder.AppendLine($"Pose: {SenseHat.Sensors.Pose?.ToString(false) ?? "N/A"}");          // From the ImuSensor.
				stringBuilder.AppendLine($"Press: {SenseHat.Sensors.Pressure?.ToString() ?? "N/A"}");          // From the PressureSensor.
				stringBuilder.AppendLine($"Temp: {SenseHat.Sensors.Temperature?.ToString() ?? "N/A"}");        // From the HumiditySensor.
				stringBuilder.AppendLine($"Hum: {SenseHat.Sensors.Humidity?.ToString() ?? "N/A"}");            // From the HumiditySensor.

				if ((SetScreenText != null) && nextMainPageUpdate <= DateTime.Now)
				{
					SetScreenText(stringBuilder.ToString());
					nextMainPageUpdate = DateTime.Now.Add(mainPageUpdateRate);
				}

				Debug.WriteLine(stringBuilder.ToString());
			}
		}
	}
}