using System;
using Windows.UI;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHatDisplay : ISenseHatDisplay
	{
		private readonly SenseHat _senseHat;
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

		// Layout of the LED buffer:
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

		public SenseHatDisplay(SenseHat senseHat)
		{
			_senseHat = senseHat;

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

		public void CopyColorsToScreen(Color[,] colors)
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
					Screen[x, y] = colors[x, y];
				}
			}
		}

		public void CopyColorsToScreen(Color[] colors)
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
					Screen[x, y] = colors[i++];
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

			_senseHat.WriteBytes(0, buffer);
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
		}

		public void Set(Color[,] pixels)
		{
		}

		public void Set(Color[] pixels)
		{
			if (pixels.Length != 64)
			{
				throw new ArgumentException("The pixel matrix must contain 64 (8x8) pixels.", nameof(pixels));
			}

			byte[] buffer = new byte[8 * 8 * 3]; // (3 for R,G,B)

			int index = 0;

			int pixelIndex = 0;

			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					Color color = pixels[pixelIndex++];

					buffer[index] = _gammaTable[color.R >> 3];
					buffer[index + 8] = _gammaTable[color.G >> 3];
					buffer[index + 16] = _gammaTable[color.B >> 3];

					index++;
				}

				index += 16; // Step to the next row.
			}

			_senseHat.WriteBytes(0, buffer);
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