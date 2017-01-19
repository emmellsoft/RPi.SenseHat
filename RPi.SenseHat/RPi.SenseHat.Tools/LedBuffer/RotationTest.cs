////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Tools
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

namespace Emmellsoft.IoT.Rpi.SenseHat.Tools.LedBuffer
{
	public enum Rotation
	{
		Deg0,
		Deg90,
		Deg180,
		Deg270,
	}

	public static class RotationTest
	{
		public static void Test()
		{
			Verify("XY" + "AB", "XY" + "AB", Rotation.Deg0, false, false);
			Verify("XY" + "AB", "YX" + "BA", Rotation.Deg0, true, false);
			Verify("XY" + "AB", "AB" + "XY", Rotation.Deg0, false, true);
			Verify("XY" + "AB", "BA" + "YX", Rotation.Deg0, true, true);

			Verify("XY" + "AB", "AX" + "BY", Rotation.Deg90, false, false);
			Verify("XY" + "AB", "XA" + "YB", Rotation.Deg90, true, false);
			Verify("XY" + "AB", "BY" + "AX", Rotation.Deg90, false, true);
			Verify("XY" + "AB", "YB" + "XA", Rotation.Deg90, true, true);

			Verify("XY" + "AB", "BA" + "YX", Rotation.Deg180, false, false);
			Verify("XY" + "AB", "AB" + "XY", Rotation.Deg180, true, false);
			Verify("XY" + "AB", "YX" + "BA", Rotation.Deg180, false, true);
			Verify("XY" + "AB", "XY" + "AB", Rotation.Deg180, true, true);

			Verify("XY" + "AB", "YB" + "XA", Rotation.Deg270, false, false);
			Verify("XY" + "AB", "BY" + "AX", Rotation.Deg270, true, false);
			Verify("XY" + "AB", "XA" + "YB", Rotation.Deg270, false, true);
			Verify("XY" + "AB", "AX" + "BY", Rotation.Deg270, true, true);
		}

		private static void Verify(string originalMapText, string expectedMapText, Rotation rotation, bool flipHorizontal, bool flipVertical)
		{
			char[,] originalMap = { { originalMapText[0], originalMapText[2] }, { originalMapText[1], originalMapText[3] } };
			char[,] expectedMap = { { expectedMapText[0], expectedMapText[2] }, { expectedMapText[1], expectedMapText[3] } };

			bool result;
			char[,] actualMap;
			try
			{
				actualMap = Process(originalMap, rotation, flipHorizontal, flipVertical);

				result = (actualMap[0, 0] == expectedMap[0, 0]) && (actualMap[0, 1] == expectedMap[0, 1]) &&
						 (actualMap[1, 0] == expectedMap[1, 0]) && (actualMap[1, 1] == expectedMap[1, 1]);
			}
			catch
			{
				actualMap = new[,] { { 'e', 'e' }, { 'e', 'e' } };
				result = false;
			}

			var what = $"{rotation}{(flipHorizontal ? "+FH" : "")}{(flipVertical ? "+FV" : "")}".PadRight(15);

			if (!result)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("{0}     {1}{2}   {3}{4}", what.PadLeft(15), expectedMap[0, 0], expectedMap[1, 0], actualMap[0, 0], actualMap[1, 0]);
				Console.WriteLine("{0}     {1}{2}   {3}{4}", "".PadLeft(15), expectedMap[0, 1], expectedMap[1, 1], actualMap[0, 1], actualMap[1, 1]);
				Console.WriteLine();
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("{0} OK", what);
			}

			Console.ForegroundColor = ConsoleColor.Gray;
		}

		private static void GetProcessInfo(
			Rotation rotation,
			bool flipHorizontal,
			bool flipVertical,
			out bool xForward,
			out bool yForward,
			out bool flipAxis)
		{
			switch (rotation)
			{
				case Rotation.Deg0:
					if (!flipHorizontal && !flipVertical)
					{
						xForward = true;
						yForward = true;
					}
					else if (flipHorizontal && !flipVertical)
					{
						xForward = false;
						yForward = true;
					}
					else if (!flipHorizontal /* && flipVertical */)
					{
						xForward = true;
						yForward = false;
					}
					else /* if (flipHorizontal && flipVertical) */
					{
						xForward = false;
						yForward = false;
					}

					flipAxis = false;
					break;
				case Rotation.Deg90:
					if (!flipHorizontal && !flipVertical)
					{
						xForward = false;
						yForward = true;
					}
					else if (flipHorizontal && !flipVertical)
					{
						xForward = true;
						yForward = true;
					}
					else if (!flipHorizontal /* && flipVertical */)
					{
						xForward = false;
						yForward = false;
					}
					else /* if (flipHorizontal && flipVertical) */
					{
						xForward = true;
						yForward = false;
					}

					flipAxis = true;
					break;
				case Rotation.Deg180:
					if (!flipHorizontal && !flipVertical)
					{
						xForward = false;
						yForward = false;
					}
					else if (flipHorizontal && !flipVertical)
					{
						xForward = true;
						yForward = false;
					}
					else if (!flipHorizontal /* && flipVertical */)
					{
						xForward = false;
						yForward = true;
					}
					else /* if (flipHorizontal && flipVertical) */
					{
						xForward = true;
						yForward = true;
					}

					flipAxis = false;
					break;
				case Rotation.Deg270:
					if (!flipHorizontal && !flipVertical)
					{
						xForward = true;
						yForward = false;
					}
					else if (flipHorizontal && !flipVertical)
					{
						xForward = false;
						yForward = false;
					}
					else if (!flipHorizontal /* && flipVertical */)
					{
						xForward = true;
						yForward = true;
					}
					else /* if (flipHorizontal && flipVertical) */
					{
						xForward = false;
						yForward = true;
					}

					flipAxis = true;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(rotation), rotation, null);
			}
		}

		private static char[,] Process(char[,] map, Rotation rotation, bool flipHorizontal, bool flipVertical)
		{
			bool xForward;
			bool yForward;
			bool flipAxis;

			GetProcessInfo(rotation, flipHorizontal, flipVertical, out xForward, out yForward, out flipAxis);

			int xStart, xStop, xStep;
			int yStart, yStop, yStep;

			if (xForward)
			{
				xStart = 0;
				xStop = 2;
				xStep = 1;
			}
			else
			{
				xStart = 1;
				xStop = -1;
				xStep = -1;
			}

			if (yForward)
			{
				yStart = 0;
				yStop = 2;
				yStep = 1;
			}
			else
			{
				yStart = 1;
				yStop = -1;
				yStep = -1;
			}

			char[,] processed = new char[2, 2];
			processed[0, 0] = '!';
			processed[1, 0] = '!';
			processed[0, 1] = '!';
			processed[1, 1] = '!';

			int toY = 0;

			for (int y = yStart; y != yStop; y += yStep)
			{
				int toX = 0;
				for (int x = xStart; x != xStop; x += xStep)
				{
					processed[toX, toY] = flipAxis ? map[y, x] : map[x, y];
					toX++;
				}

				toY++;
			}

			return processed;
		}
	}
}