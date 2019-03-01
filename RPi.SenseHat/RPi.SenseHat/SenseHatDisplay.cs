////////////////////////////////////////////////////////////////////////////
//
//  This file is part of RPi.SenseHat
//
//  Copyright (c) 2019, Mattias Larsson
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
using System.Linq;

namespace Emmellsoft.IoT.RPi.SenseHat
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
        private byte[] _redGammaTable;
        private byte[] _greenGammaTable;
        private byte[] _blueGammaTable;

        private static readonly Color[,] InitialColors =
        {
            { Color.FromRgb(0xfb0000), Color.FromRgb(0xfb0000), Color.FromRgb(0xfb5600), Color.FromRgb(0xfbc400), Color.FromRgb(0xcdfb00), Color.FromRgb(0x56fb00), Color.FromRgb(0x00fb00), Color.FromRgb(0x00fb7f) },
            { Color.FromRgb(0xfb0000), Color.FromRgb(0xfb6200), Color.FromRgb(0xfbcd00), Color.FromRgb(0xc4fb00), Color.FromRgb(0x56fb00), Color.FromRgb(0x00fb00), Color.FromRgb(0x00fb87), Color.FromRgb(0x00fbf4) },
            { Color.FromRgb(0xfb6c00), Color.FromRgb(0xfbd200), Color.FromRgb(0xbffb00), Color.FromRgb(0x47fb00), Color.FromRgb(0x00fb00), Color.FromRgb(0x00fb8e), Color.FromRgb(0x00fbfb), Color.FromRgb(0x009cfb) },
            { Color.FromRgb(0xfbdb00), Color.FromRgb(0xb4fb00), Color.FromRgb(0x47fb00), Color.FromRgb(0x00fb00), Color.FromRgb(0x00fb95), Color.FromRgb(0x00fbfb), Color.FromRgb(0x0095fb), Color.FromRgb(0x0000fb) },
            { Color.FromRgb(0xaefb00), Color.FromRgb(0x34fb00), Color.FromRgb(0x00fb34), Color.FromRgb(0x00fb9c), Color.FromRgb(0x00f4fb), Color.FromRgb(0x0087fb), Color.FromRgb(0x0000fb), Color.FromRgb(0x5600fb) },
            { Color.FromRgb(0x34fb00), Color.FromRgb(0x00fb34), Color.FromRgb(0x00fba2), Color.FromRgb(0x00ecfb), Color.FromRgb(0x007ffb), Color.FromRgb(0x0000fb), Color.FromRgb(0x5600fb), Color.FromRgb(0xcd00fb) },
            { Color.FromRgb(0x00fb47), Color.FromRgb(0x00fbae), Color.FromRgb(0x00e4fb), Color.FromRgb(0x0076fb), Color.FromRgb(0x0000fb), Color.FromRgb(0x6200fb), Color.FromRgb(0xd200fb), Color.FromRgb(0xfb00c4) },
            { Color.FromRgb(0x00fbb4), Color.FromRgb(0x00dbfb), Color.FromRgb(0x006cfb), Color.FromRgb(0x0000fb), Color.FromRgb(0x6c00fb), Color.FromRgb(0xdb00fb), Color.FromRgb(0xfb00b4), Color.FromRgb(0xfb0047) },
        };

        private double _redGamma;
        private double _greenGamma;
        private double _blueGamma;

        public SenseHatDisplay(MainI2CDevice mainI2CDevice)
        {
            _mainI2CDevice = mainI2CDevice;

            Screen = new Image(8, 8);

            RedGamma = 1.8;
            GreenGamma = 2.0;
            BlueGamma = 1.8;

            Reset();

            UpdateDirectionParameters();
        }

        public Image Screen { get; }

        public DisplayDirection Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                UpdateDirectionParameters();
            }
        }

        public bool FlipHorizontal
        {
            get => _flipHorizontal;
            set
            {
                _flipHorizontal = value;
                UpdateDirectionParameters();
            }
        }

        public bool FlipVertical
        {
            get => _flipVertical;
            set
            {
                _flipVertical = value;
                UpdateDirectionParameters();
            }
        }

        public double RedGamma
        {
            get => _redGamma;
            set
            {
                _redGamma = value;
                _redGammaTable = Get5BitGammaTable(_redGamma).ToArray();
            }
        }

        public double GreenGamma
        {
            get => _greenGamma;
            set
            {
                _greenGamma = value;
                _greenGammaTable = Get5BitGammaTable(_greenGamma).ToArray();
            }
        }

        public double BlueGamma
        {
            get => _blueGamma;
            set
            {
                _blueGamma = value;
                _blueGammaTable = Get5BitGammaTable(_blueGamma).ToArray();
            }
        }

        public void Reset()
        {
            Array.Copy(InitialColors, Screen.Pixels, Screen.Length);
        }

        public void Clear()
        {
            Screen.Fill(Color.FromRgb(0, 0, 0));
        }

        public void Fill(Color color)
        {
            Screen.Fill(color);
        }

        public void CopyColorsToScreen(Image image, int offsetX, int offsetY)
        {
            AssertImageSize(image);
            AssertScreenSize();

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
                    Screen[(x + offsetX) % 8, (y + offsetY) % 8] = image[x, y];
                }
            }
        }

        private void AssertScreenSize()
        {
            if ((Screen.Width != 8) || (Screen.Height != 8))
            {
                // Hm. Someone has messed with the pixels array...
                throw new ArgumentException("My image must be 8x8.");
            }
        }

        private static void AssertImageSize(Image image)
        {
            if ((image.Width != 8) || (image.Height != 8))
            {
                throw new ArgumentException("The image must be 8x8.", nameof(image));
            }
        }

        public void CopyColorsToScreen(Color[] colors, int offsetX, int offsetY)
        {
            if (colors.Length != 64)
            {
                throw new ArgumentException("The pixel array must be 64 'pixels' long (i.e. 8x8).", nameof(colors));
            }

            AssertScreenSize();

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

        public void CopyScreenToColors(Image image)
        {
            AssertImageSize(image);
            AssertScreenSize();

            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    image[x, y] = Screen[x, y];
                }
            }
        }

        public void CopyScreenToColors(Color[] colors)
        {
            if (colors.Length != 64)
            {
                throw new ArgumentException("The pixel array must be 64 'pixels' long (i.e. 8x8).", nameof(colors));
            }
            AssertScreenSize();

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

        /// <summary>
        /// The layout of the LED buffer:
        /// Row 1: R R R R R R R R G G G G G G G G B B B B B B B B
        /// Row 2: R R R R R R R R G G G G G G G G B B B B B B B B
        /// ...
        /// Row 8: R R R R R R R R G G G G G G G G B B B B B B B B
        /// </summary>
        public void Update()
        {
            AssertScreenSize();

            byte[] buffer = new byte[8 * 8 * 3];

            int index = 0;
            for (int y = _yStart; y != _yStop; y += _yStep)
            {
                for (int x = _xStart; x != _xStop; x += _xStep)
                {
                    Color color = _getPixel(x, y);

                    buffer[index] = _redGammaTable[ScaleTo5Bit(color.R)];
                    buffer[index + 8] = _greenGammaTable[ScaleTo5Bit(color.G)];
                    buffer[index + 16] = _blueGammaTable[ScaleTo5Bit(color.B)];

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

        private static byte ScaleTo5Bit(byte byteValue)
        {
            int fiveBit = byteValue >> 3;

            if ((fiveBit < 0x1b) && ((byteValue & 0x04) == 0x04))
            {
                fiveBit++; // Round up.
            }

            return (byte)fiveBit;
        }

        private void UpdateDirectionParameters()
        {
            PixelSupport.ConvertDirectionParameters(
                _direction,
                _flipHorizontal,
                _flipVertical,
                out bool leftToRight,
                out bool topToBottom,
                out bool flipAxis);

            if (leftToRight)
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

            if (topToBottom)
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

        private static IEnumerable<byte> Get5BitGammaTable(double gamma)
        {
            const double step = 255.0 / 31; // 8 bits -> 5 bits

            for (int i = 0; i <= 31; i++)
            {
                byte index = (byte)(i * step);

                double gammaFactor = Math.Pow(index / 255.0, gamma);

                byte gammaByte = (byte)Math.Min((int)Math.Round(gammaFactor * 255.0 / 8.0), 31);

                yield return gammaByte;
            }
        }
    }
}
