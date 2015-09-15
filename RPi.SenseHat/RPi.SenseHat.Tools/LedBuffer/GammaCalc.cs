using System;
using System.Collections.Generic;
using System.Linq;

namespace Emmellsoft.IoT.Rpi.SenseHat.Tools.LedBuffer
{
	public class GammaCalc
	{
		private const double DefaultGamma = 2.8;

		public static IEnumerable<double> GetGamma(double gamma = DefaultGamma)
		{
			for (int i = 0; i <= 255; i++)
			{
				yield return Math.Pow((double)i / 255, gamma);
			}
		}

		public static IEnumerable<byte> ScaleToBytes(IEnumerable<double> values)
		{
			return values.Select(ScaleToByte);
		}

		public static byte ScaleToByte(double value)
		{
			int rounded = (int)Math.Round(value * 255);

			if ((rounded < 0) || (rounded > 255))
			{
				throw new ArgumentException("Not a byte: " + value);
			}

			return (byte)rounded;
		}

		public static IEnumerable<byte> Get5BitGamma(double gamma = DefaultGamma)
		{
			double[] gammaTable = GetGamma(gamma).ToArray();

			const double step = 255.0 / 31; // 8 bits -> 5 bits

			for (int i = 0; i < 32; i++)
			{
				byte index = (byte)(i * step);

				double gammaFactor = gammaTable[index];

				byte gammaByte = (byte)Math.Min((int)ScaleToByte(gammaFactor / 8), 31);

				yield return gammaByte;
			}
		}

		public static IEnumerable<byte> Get5To8BitInvertedGamma(double gamma = DefaultGamma)
		{
			List<double> gammaTable = GetGamma(gamma).ToList();

			const double step = 1.0 / 32; // 8 bits -> 5 bits

			for (int i = 0; i < 32; i++)
			{
				double want = i * step;

				double? floor = gammaTable.Where(x => x <= want).Select(x => (double?)x).LastOrDefault();
				double? ceil = gammaTable.Where(x => x >= want).Select(x => (double?)x).FirstOrDefault();

				double floorDiff = floor.HasValue ? Math.Abs(want - floor.Value) : double.MaxValue;
				double ceilDiff = ceil.HasValue ? Math.Abs(want - ceil.Value) : double.MaxValue;

				if ((floorDiff <= ceilDiff) && floor.HasValue)
                {
					yield return (byte)gammaTable.IndexOf(floor.Value);
				}
				else if (ceil.HasValue)
				{
					yield return (byte)gammaTable.IndexOf(ceil.Value);
				}
				else
				{
					throw new Exception();
				}
			}
		}
	}
}