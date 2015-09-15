using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts;
using Emmellsoft.IoT.Rpi.SenseHat.Tools.Font;
using Emmellsoft.IoT.Rpi.SenseHat.Tools.LedBuffer;

namespace Emmellsoft.IoT.Rpi.SenseHat.Tools
{
	class Program
	{
		static void Main(string[] args)
		{
			LedBufferWork();

			BwFontWork();
		}

		private static void BwFontWork()
		{
			var bitmap = new Bitmap(@"Font\BWFont.png");
			const string chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖÉÜabcdefghijklmnopqrstuvwxyzåäöéü0123456789.,?!\"#$%&-+*:;/\\<>()'`=";

			BwFont bwFont = BwFontBuilder.GetBwFont(bitmap, chars);
			byte[] fontBytes = bwFont.Serialize().ToArray();
			var fontBytesAsCode = ToCSharp(fontBytes);

			Tuple<string, Bitmap> tuple = BwFontBuilder.GetFontBitmap(fontBytes);
			tuple.Item2.Save(@"Font\BWFont_recreated.png");
		}

		private static void LedBufferWork()
		{
			byte[] originalGamma =
			{
				0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01,
				0x02, 0x02, 0x03, 0x03, 0x04, 0x05, 0x06, 0x07,
				0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0E, 0x0F, 0x11,
				0x12, 0x14, 0x15, 0x17, 0x19, 0x1B, 0x1D, 0x1F
			};

			double senseHatGamma = FindBestGammaMatch.Best5BitGammaMatch(originalGamma, 2, 3, 0.01);

			byte[] senseHatGammaTable = GammaCalc.Get5BitGamma(senseHatGamma).ToArray();
			string senseHatGammaTableAsCode = ToCSharp(senseHatGammaTable);

			byte[] senseHatInverseGammaTable = GammaCalc.Get5To8BitInvertedGamma(senseHatGamma).ToArray();
			string senseHatInverseGammaTableAsCode = ToCSharp(senseHatInverseGammaTable);

			var initalPixels = LedBufferSupport.BufferToPixels(LedBufferSupport.GetInitialLedBuffer(), senseHatGamma);
			var initalPixelsAsCode = ToCSharp(initalPixels);

			RotationTest.Test();
		}

		private static string ToCSharp(IEnumerable<byte> bytes)
		{
			return string.Join(", ", bytes.Select(ByteToCSharp));
			//return string.Join(",", bytes.Select(g => g.ToString().PadLeft(3)));
			//return String.Join("  ", bytes.Select(g => g.ToString("x2")));
		}

		private static string ToCSharp(Color[,] pixels)
		{
			var text = new StringBuilder();
			text.AppendLine("Color[,] colors = new Color[,]");
			text.AppendLine("{");

			for (int y = 0; y < 8; y++)
			{
				text.Append("\t{ ");
				for (int x = 0; x < 8; x++)
				{
					Color color = pixels[x, y];

					if (x > 0)
					{
						text.Append(", ");
					}

					text.Append("Color.FromArgb(0xff, ");
					text.Append(string.Join(", ", new[] { color.R, color.G, color.B }.Select(ByteToCSharp)));
					text.Append(")");
				}
				text.AppendLine(" },");
			}

			text.AppendLine("};");

			return text.ToString();
		}

		private static string ByteToCSharp(byte value)
		{
			return "0x" + value.ToString("x2");
		}
	}
}
