////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Tools
//
//  Copyright (c) 2015, Mattias Larsson
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
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.BW;
using Emmellsoft.IoT.Rpi.SenseHat.Tools.Font;
using Emmellsoft.IoT.Rpi.SenseHat.Tools.LedBuffer;

namespace Emmellsoft.IoT.Rpi.SenseHat.Tools
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			LedBufferWork();

			BwFontWork();
			BwTinyFontWork();
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

		private static void BwTinyFontWork()
		{
			var bitmap = new Bitmap(@"Font\TinyBWFont.png");
			const string chars = " 0123456789ABCDEF+-%*=.:!?/\\'";

			BwFont tinyFont = BwFontBuilder.GetBwFont(bitmap, chars);
			byte[] fontBytes = tinyFont.Serialize().ToArray();
			var fontBytesAsCode = ToCSharp(fontBytes);
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



			Color[,] initalPixels = LedBufferSupport.BufferToPixels(LedBufferSupport.GetInitialLedBuffer(), senseHatGamma);
			string initalPixelsAsCode = ToCSharp(initalPixels);

			byte[] initialBufferRecreated = LedBufferSupport.PixelsToBuffer(initalPixels, senseHatGamma);
			string initialBufferRecreatedAsCode = ToCSharp(initialBufferRecreated);
			string originalInitialBufferAsCode = ToCSharp(LedBufferSupport.GetInitialLedBuffer());

			var xxx = initialBufferRecreatedAsCode + "\r\n" + originalInitialBufferAsCode;

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
