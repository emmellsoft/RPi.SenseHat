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
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public static class PixelSupport
	{
		public static Task<Color[,]> GetPixels(Uri imageUri)
		{
			if (imageUri.IsFile)
			{
				if (!File.Exists(imageUri.LocalPath))
				{
					throw new ArgumentException("File Missing: " + imageUri.LocalPath);
				}

				Bitmap bitmap = new Bitmap(imageUri.LocalPath);

				Color[,] pixels = new Color[bitmap.Width, bitmap.Height];
				for (int y = 0; y < bitmap.Height; y++)
				{
					for (int x = 0; x < bitmap.Width; x++)
					{
						pixels[x, y] = bitmap.GetPixel(x, y);
					}
				}

				return Task.FromResult(pixels);
			}

			throw new NotImplementedException();
		}
	}
}