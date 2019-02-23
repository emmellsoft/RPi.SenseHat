////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Tools
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
using System.Drawing;
using System.IO;

namespace Emmellsoft.IoT.Rpi.SenseHat.Tools
{
    public static class ImageSupport
    {
        public static Image GetImage(string path)
        {
            if (!File.Exists(path))
            {
                throw new ArgumentException("File Missing: " + path);
            }

            Bitmap bitmap = new Bitmap(path);

            Image image = new Image(bitmap.Width, bitmap.Height);
            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    image[x, y] = bitmap.GetPixel(x, y).ToSenseColor();
                }
            }

            return image;
        }
    }
}
