////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat
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
using Windows.UI;

namespace Emmellsoft.IoT.Rpi.SenseHat.Sprites
{
	public class SpriteMap
	{
		public SpriteMap(Color[,] pixels)
		{
			if (((pixels.GetLength(0) % 8) != 0) || ((pixels.GetLength(1) % 8) != 0))
			{
				throw new ArgumentException("Expecting an image with sides of mutiple of 8 pixels");
			}

			Pixels = pixels;

			SpriteCountHorizontal = Pixels.GetLength(0) / 8;
			SpriteCountVertical = Pixels.GetLength(1) / 8;
		}

		internal Color[,] Pixels
		{ get; }

		public int SpriteCountHorizontal
		{ get; }

		public int SpriteCountVertical
		{ get; }

		public Sprite GetSprite(int index)
		{
			int xIndex = index % 8;
			int yIndex = index / 8;

			if ((xIndex < 0) || (xIndex >= SpriteCountHorizontal) ||
				(yIndex < 0) || (yIndex >= SpriteCountVertical))
			{
				throw new IndexOutOfRangeException("The sprite index is out of range!");
			}

			return new Sprite(this, xIndex, yIndex);
		}

		public Sprite GetSprite(int xIndex, int yIndex)
		{
			if ((xIndex < 0) || (xIndex >= SpriteCountHorizontal) ||
				(yIndex < 0) || (yIndex >= SpriteCountVertical))
			{
				throw new IndexOutOfRangeException("The sprite index is out of range!");
			}

			return new Sprite(this, xIndex, yIndex);
		}
	}
}