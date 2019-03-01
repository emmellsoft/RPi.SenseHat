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

namespace Emmellsoft.IoT.RPi.SenseHat
{
    public static class PixelSupport
    {
        internal static void ConvertDirectionParameters(
            DisplayDirection direction,
            bool flipHorizontal,
            bool flipVertical,
            out bool leftToRight,
            out bool topToBottom,
            out bool flipAxis)
        {
            switch (direction)
            {
                case DisplayDirection.Deg0:
                    if (!flipHorizontal && !flipVertical)
                    {
                        leftToRight = true;
                        topToBottom = true;
                    }
                    else if (flipHorizontal && !flipVertical)
                    {
                        leftToRight = false;
                        topToBottom = true;
                    }
                    else if (!flipHorizontal /* && flipVertical */)
                    {
                        leftToRight = true;
                        topToBottom = false;
                    }
                    else /* if (flipHorizontal && flipVertical) */
                    {
                        leftToRight = false;
                        topToBottom = false;
                    }

                    flipAxis = false;
                    break;

                case DisplayDirection.Deg90:
                    if (!flipHorizontal && !flipVertical)
                    {
                        leftToRight = false;
                        topToBottom = true;
                    }
                    else if (flipHorizontal && !flipVertical)
                    {
                        leftToRight = true;
                        topToBottom = true;
                    }
                    else if (!flipHorizontal /* && flipVertical */)
                    {
                        leftToRight = false;
                        topToBottom = false;
                    }
                    else /* if (flipHorizontal && flipVertical) */
                    {
                        leftToRight = true;
                        topToBottom = false;
                    }

                    flipAxis = true;
                    break;

                case DisplayDirection.Deg180:
                    if (!flipHorizontal && !flipVertical)
                    {
                        leftToRight = false;
                        topToBottom = false;
                    }
                    else if (flipHorizontal && !flipVertical)
                    {
                        leftToRight = true;
                        topToBottom = false;
                    }
                    else if (!flipHorizontal /* && flipVertical */)
                    {
                        leftToRight = false;
                        topToBottom = true;
                    }
                    else /* if (flipHorizontal && flipVertical) */
                    {
                        leftToRight = true;
                        topToBottom = true;
                    }

                    flipAxis = false;
                    break;

                case DisplayDirection.Deg270:
                    if (!flipHorizontal && !flipVertical)
                    {
                        leftToRight = true;
                        topToBottom = false;
                    }
                    else if (flipHorizontal && !flipVertical)
                    {
                        leftToRight = false;
                        topToBottom = false;
                    }
                    else if (!flipHorizontal /* && flipVertical */)
                    {
                        leftToRight = true;
                        topToBottom = true;
                    }
                    else /* if (flipHorizontal && flipVertical) */
                    {
                        leftToRight = false;
                        topToBottom = true;
                    }

                    flipAxis = true;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Image Convert1DToImage(Color[] pixels)
        {
            if (pixels.Length != 64)
            {
                throw new ArgumentException("The pixel array must be 64 bytes long (i.e. 8x8).", nameof(pixels));
            }

            var image = new Image(8, 8);

            int i = 0;
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    image[x, y] = pixels[i++];
                }
            }

            return image;
        }
    }
}
