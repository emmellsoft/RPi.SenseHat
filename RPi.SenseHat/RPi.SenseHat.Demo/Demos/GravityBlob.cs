using System;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using RichardsTech.Sensors;

namespace RPi.SenseHat.Demo.Demos
{
	public sealed class GravityBlob : SenseHatDemo
	{
		public GravityBlob(ISenseHat senseHat):base(senseHat)
		{
		}

		public override void Update()
		{
			if (!SenseHat.Sensors.ImuSensor.Update())
			{
				return;
			}

			if (!SenseHat.Sensors.Acceleration.HasValue)
			{
				return;
			}

			Color[,] colors = CreateGravityBlobScreen(SenseHat.Sensors.Acceleration.Value);

			SenseHat.Display.CopyColorsToScreen(colors);

			SenseHat.Display.Update();
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