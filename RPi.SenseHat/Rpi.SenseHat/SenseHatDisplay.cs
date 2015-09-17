////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat
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
using System.Linq;
using Windows.UI;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHatDisplay : ISenseHatDisplay
	{
		private readonly MainI2CDevice _mainI2CDevice;
		private DisplayDirection _direction;
		private bool _flipHorizontal;
		private bool _flipVertical;
		private int _xStart;
		private int _xStop;
		private int _xStep;
		private int _yStart;
		private int _yStop;
		private int _yStep;
		private Func<int, int, Color> _getPixel;

		// The layout of the LED buffer:
		// Row 1: R R R R R R R R G G G G G G G G B B B B B B B B
		// Row 2: R R R R R R R R G G G G G G G G B B B B B B B B
		// ...
		// Row 8: R R R R R R R R G G G G G G G G B B B B B B B B

		private readonly byte[] _gammaTable =
		{
			0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01,
			0x02, 0x02, 0x03, 0x03, 0x04, 0x05, 0x06, 0x07,
			0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0e, 0x0f, 0x11,
			0x12, 0x14, 0x16, 0x18, 0x19, 0x1b, 0x1d, 0x1f
		};

		private readonly byte[] _ledValueTo8BitTable =
		{
			0x00, 0x34, 0x47, 0x56, 0x62, 0x6c, 0x76, 0x7f,
			0x87, 0x8e, 0x95, 0x9c, 0xa2, 0xa8, 0xae, 0xb4,
			0xb9, 0xbf, 0xc4, 0xc9, 0xcd, 0xd2, 0xd7, 0xdb,
			0xdf, 0xe4, 0xe8, 0xec, 0xf0, 0xf4, 0xf8, 0xfb
		};

		private readonly Color[,] _initialColors =
		{
			{ Color.FromArgb(0xff, 0xfb, 0x00, 0x00), Color.FromArgb(0xff, 0xfb, 0x00, 0x00), Color.FromArgb(0xff, 0xfb, 0x56, 0x00), Color.FromArgb(0xff, 0xfb, 0xc4, 0x00), Color.FromArgb(0xff, 0xcd, 0xfb, 0x00), Color.FromArgb(0xff, 0x56, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x7f) },
			{ Color.FromArgb(0xff, 0xfb, 0x00, 0x00), Color.FromArgb(0xff, 0xfb, 0x62, 0x00), Color.FromArgb(0xff, 0xfb, 0xcd, 0x00), Color.FromArgb(0xff, 0xc4, 0xfb, 0x00), Color.FromArgb(0xff, 0x56, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x87), Color.FromArgb(0xff, 0x00, 0xfb, 0xf4) },
			{ Color.FromArgb(0xff, 0xfb, 0x6c, 0x00), Color.FromArgb(0xff, 0xfb, 0xd2, 0x00), Color.FromArgb(0xff, 0xbf, 0xfb, 0x00), Color.FromArgb(0xff, 0x47, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x8e), Color.FromArgb(0xff, 0x00, 0xfb, 0xfb), Color.FromArgb(0xff, 0x00, 0x9c, 0xfb) },
			{ Color.FromArgb(0xff, 0xfb, 0xdb, 0x00), Color.FromArgb(0xff, 0xb4, 0xfb, 0x00), Color.FromArgb(0xff, 0x47, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x95), Color.FromArgb(0xff, 0x00, 0xfb, 0xfb), Color.FromArgb(0xff, 0x00, 0x95, 0xfb), Color.FromArgb(0xff, 0x00, 0x00, 0xfb) },
			{ Color.FromArgb(0xff, 0xae, 0xfb, 0x00), Color.FromArgb(0xff, 0x34, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x34), Color.FromArgb(0xff, 0x00, 0xfb, 0x9c), Color.FromArgb(0xff, 0x00, 0xf4, 0xfb), Color.FromArgb(0xff, 0x00, 0x87, 0xfb), Color.FromArgb(0xff, 0x00, 0x00, 0xfb), Color.FromArgb(0xff, 0x56, 0x00, 0xfb) },
			{ Color.FromArgb(0xff, 0x34, 0xfb, 0x00), Color.FromArgb(0xff, 0x00, 0xfb, 0x34), Color.FromArgb(0xff, 0x00, 0xfb, 0xa2), Color.FromArgb(0xff, 0x00, 0xec, 0xfb), Color.FromArgb(0xff, 0x00, 0x7f, 0xfb), Color.FromArgb(0xff, 0x00, 0x00, 0xfb), Color.FromArgb(0xff, 0x56, 0x00, 0xfb), Color.FromArgb(0xff, 0xcd, 0x00, 0xfb) },
			{ Color.FromArgb(0xff, 0x00, 0xfb, 0x47), Color.FromArgb(0xff, 0x00, 0xfb, 0xae), Color.FromArgb(0xff, 0x00, 0xe4, 0xfb), Color.FromArgb(0xff, 0x00, 0x76, 0xfb), Color.FromArgb(0xff, 0x00, 0x00, 0xfb), Color.FromArgb(0xff, 0x62, 0x00, 0xfb), Color.FromArgb(0xff, 0xd2, 0x00, 0xfb), Color.FromArgb(0xff, 0xfb, 0x00, 0xc4) },
			{ Color.FromArgb(0xff, 0x00, 0xfb, 0xb4), Color.FromArgb(0xff, 0x00, 0xdb, 0xfb), Color.FromArgb(0xff, 0x00, 0x6c, 0xfb), Color.FromArgb(0xff, 0x00, 0x00, 0xfb), Color.FromArgb(0xff, 0x6c, 0x00, 0xfb), Color.FromArgb(0xff, 0xdb, 0x00, 0xfb), Color.FromArgb(0xff, 0xfb, 0x00, 0xb4), Color.FromArgb(0xff, 0xfb, 0x00, 0x47) },
		};

		public SenseHatDisplay(MainI2CDevice mainI2CDevice)
		{
			_mainI2CDevice = mainI2CDevice;

			Screen = new Color[8, 8];

			Reset();

			UpdateDirectionParameters();
		}

		public Color[,] Screen
		{ get; }

		public DisplayDirection Direction
		{
			get { return _direction; }
			set
			{
				_direction = value;
				UpdateDirectionParameters();
			}
		}

		public bool FlipHorizontal
		{
			get { return _flipHorizontal; }
			set
			{
				_flipHorizontal = value;
				UpdateDirectionParameters();
			}
		}

		public bool FlipVertical
		{
			get { return _flipVertical; }
			set
			{
				_flipVertical = value;
				UpdateDirectionParameters();
			}
		}

		public void Reset()
		{
			Array.Copy(_initialColors, Screen, Screen.Length);
		}

		public void Clear()
		{
			Fill(Colors.Black);
		}

		public void Fill(Color color)
		{
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					Screen[x, y] = color;
				}
			}
		}

		public void CopyColorsToScreen(Color[,] colors, int offsetX, int offsetY)
		{
			if ((colors.GetLength(0) != 8) || (colors.GetLength(1) != 8))
			{
				throw new ArgumentException("The pixel matrix must be 8x8.", nameof(colors));
			}

			if ((Screen.GetLength(0) != 8) || (Screen.GetLength(1) != 8))
			{
				// Hm. Someone has messed with the pixels array...
				throw new ArgumentException("My pixel matrix must be 8x8.");
			}

			if (offsetX < 0)
			{
				offsetX = 8 + (offsetX % 8);
			}

			if (offsetY < 0)
			{
				offsetY = 8 + (offsetY % 8);
			}

			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					Screen[(x + offsetX) % 8, (y + offsetY) % 8] = colors[x, y];
				}
			}
		}

		public void CopyColorsToScreen(Color[] colors, int offsetX, int offsetY)
		{
			if (colors.Length != 64)
			{
				throw new ArgumentException("The pixel array must be 64 'pixels' long (i.e. 8x8).", nameof(colors));
			}

			if ((Screen.GetLength(0) != 8) || (Screen.GetLength(1) != 8))
			{
				// Hm. Someone has messed with the pixels array...
				throw new ArgumentException("My pixel matrix must be 8x8.");
			}

			if (offsetX < 0)
			{
				offsetX = 8 + (offsetX % 8);
			}

			if (offsetY < 0)
			{
				offsetY = 8 + (offsetY % 8);
			}

			int i = 0;
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					Screen[(x + offsetX) % 8, (y + offsetY) % 8] = colors[i++];
				}
			}
		}

		public void CopyScreenToColors(Color[,] colors)
		{
			if ((colors.GetLength(0) != 8) || (colors.GetLength(1) != 8))
			{
				throw new ArgumentException("The pixel matrix must be 8x8.", nameof(colors));
			}

			if ((Screen.GetLength(0) != 8) || (Screen.GetLength(1) != 8))
			{
				// Hm. Someone has messed with the pixels array...
				throw new ArgumentException("My pixel matrix must be 8x8.");
			}

			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					colors[x, y] = Screen[x, y];
				}
			}
		}

		public void CopyScreenToColors(Color[] colors)
		{
			if (colors.Length != 64)
			{
				throw new ArgumentException("The pixel array must be 64 'pixels' long (i.e. 8x8).", nameof(colors));
			}

			if ((Screen.GetLength(0) != 8) || (Screen.GetLength(1) != 8))
			{
				// Hm. Someone has messed with the pixels array...
				throw new ArgumentException("My pixel matrix must be 8x8.");
			}

			int i = 0;
			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					colors[i++] = Screen[x, y];
				}
			}
		}

		public byte[] ReadRaw()
		{
			return _mainI2CDevice.ReadBytes(0, 192);
		}

		public void WriteRaw(byte[] rawBuffer)
		{
			if (rawBuffer.Length != 192)
			{
				throw new ArgumentException("The raw pixel array must be 192 bytes long (i.e. 8x8x3).", nameof(rawBuffer));
			}

			_mainI2CDevice.WriteBytes(0, rawBuffer);
		}

		public void Update()
		{
			if ((Screen.GetLength(0) != 8) || (Screen.GetLength(1) != 8))
			{
				// Hm. Someone has messed with the pixels array...
				throw new ArgumentException("My pixel matrix must be 8x8.");
			}

			byte[] buffer = new byte[8 * 8 * 3];

			int index = 0;
			for (int y = _yStart; y != _yStop; y += _yStep)
			{
				for (int x = _xStart; x != _xStop; x += _xStep)
				{
					Color color = _getPixel(x, y);

					buffer[index] = ScaleAndGammaCorrect(color.R);
					buffer[index + 8] = ScaleAndGammaCorrect(color.G);
					buffer[index + 16] = ScaleAndGammaCorrect(color.B);

					index++;
				}

				index += 16; // Step to the next row.
			}

			_mainI2CDevice.WriteBytes(0, buffer);
		}

		private Color GetPixel(int x, int y)
		{
			return Screen[x, y];
		}

		private Color GetPixelFlipAxis(int x, int y)
		{
			return Screen[y, x];
		}

		private byte ScaleAndGammaCorrect(byte byteValue)
		{
			int fiveBit = byteValue >> 3;

			if ((fiveBit < 0x1b) && ((byteValue & 0x04) == 0x04))
			{
				fiveBit++; // Round up.
			}

			return _gammaTable[fiveBit];
			//return (byte)fiveBit;
		}

		private void UpdateDirectionParameters()
		{
			bool xForward;
			bool yForward;
			bool flipAxis;

			switch (_direction)
			{
				case DisplayDirection.Deg0:
					if (!_flipHorizontal && !_flipVertical)
					{
						xForward = true;
						yForward = true;
					}
					else if (_flipHorizontal && !_flipVertical)
					{
						xForward = false;
						yForward = true;
					}
					else if (!_flipHorizontal /* && flipVertical */)
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

				case DisplayDirection.Deg90:
					if (!_flipHorizontal && !_flipVertical)
					{
						xForward = false;
						yForward = true;
					}
					else if (_flipHorizontal && !_flipVertical)
					{
						xForward = true;
						yForward = true;
					}
					else if (!_flipHorizontal /* && flipVertical */)
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

				case DisplayDirection.Deg180:
					if (!_flipHorizontal && !_flipVertical)
					{
						xForward = false;
						yForward = false;
					}
					else if (_flipHorizontal && !_flipVertical)
					{
						xForward = true;
						yForward = false;
					}
					else if (!_flipHorizontal /* && flipVertical */)
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

				case DisplayDirection.Deg270:
					if (!_flipHorizontal && !_flipVertical)
					{
						xForward = true;
						yForward = false;
					}
					else if (_flipHorizontal && !_flipVertical)
					{
						xForward = false;
						yForward = false;
					}
					else if (!_flipHorizontal /* && flipVertical */)
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
					throw new ArgumentOutOfRangeException(nameof(_direction), _direction, null);
			}

			if (xForward)
			{
				_xStart = 0;
				_xStop = 8;
				_xStep = 1;
			}
			else
			{
				_xStart = 7;
				_xStop = -1;
				_xStep = -1;
			}

			if (yForward)
			{
				_yStart = 0;
				_yStop = 8;
				_yStep = 1;
			}
			else
			{
				_yStart = 7;
				_yStop = -1;
				_yStep = -1;
			}

			_getPixel = flipAxis
				? (Func<int, int, Color>)GetPixelFlipAxis
				: GetPixel;
		}
	}
}