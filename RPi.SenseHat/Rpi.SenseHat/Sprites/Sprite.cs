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
	public class Sprite
	{
		private readonly SpriteMap _spriteMap;
		private readonly int _spriteXIndex;
		private readonly int _spriteYIndex;

		internal Sprite(SpriteMap spriteMap, int spriteXIndex, int spriteYIndex)
		{
			_spriteMap = spriteMap;
			_spriteXIndex = spriteXIndex;
			_spriteYIndex = spriteYIndex;
		}

		public void Draw(
			ISenseHatDisplay display,
			int offsetX,
			int offsetY,
			bool transparent,
			DisplayDirection direction,
			bool flipHorizontal,
			bool flipVertical)
		{
			int right = offsetX + 7;
			int bottom = offsetY + 7;

			if ((offsetY > 7) || (bottom < 0) || (offsetX > 7) || (right < 0))
			{
				return;
			}

			int spritePixelX0 = _spriteXIndex * 8;
			int spritePixelY0 = _spriteYIndex * 8;

			int spritePixelXOffset;
			int spritePixelYOffset;

			if (offsetX < 0)
			{
				spritePixelXOffset = -offsetX;
				offsetX = 0;
			}
			else
			{
				spritePixelXOffset = 0;
			}

			if (offsetY < 0)
			{
				spritePixelYOffset = -offsetY;
				offsetY = 0;
			}
			else
			{
				spritePixelYOffset = 0;
			}
			if (right > 7)
			{
				right = 7;
			}

			if (bottom > 7)
			{
				bottom = 7;
			}

			bool leftToRight;
			bool topToBottom;
			bool flipAxis;

			PixelSupport.ConvertDirectionParameters(
				direction,
				flipHorizontal,
				flipVertical,
				out leftToRight,
				out topToBottom,
				out flipAxis);

			int xStart;
			int xStep;
			int yStart;
			int yStep;

			if (leftToRight)
			{
				xStart = 0;
				xStep = 1;
			}
			else
			{
				xStart = 7;
				xStep = -1;
				spritePixelXOffset = -spritePixelXOffset;
			}

			if (topToBottom)
			{
				yStart = 0;
				yStep = 1;
			}
			else
			{
				yStart = 7;
				yStep = -1;
				spritePixelYOffset = -spritePixelYOffset;
			}

			int spritePixelY = yStart;

			for (int screenY = offsetY; screenY <= bottom; screenY++)
			{
				int spritePixelX = xStart;

				for (int screenX = offsetX; screenX <= right; screenX++)
				{
					int dX = flipHorizontal
						? 7 - spritePixelXOffset - spritePixelX
						: spritePixelXOffset + spritePixelX;

					int dY = flipVertical
						? 7 - spritePixelYOffset - spritePixelY
						: spritePixelYOffset + spritePixelY;

					Color spriteColor = flipAxis
						? _spriteMap.Pixels[spritePixelX0 + dY, spritePixelY0 + dX]
						: _spriteMap.Pixels[spritePixelX0 + dX, spritePixelY0 + dY];

					if (transparent)
					{
						Color screenColor = display.Screen[screenX, screenY];

						spriteColor = GetTransparentPixel(screenColor, spriteColor);
					}

					display.Screen[screenX, screenY] = spriteColor;

					spritePixelX += xStep;
				}

				spritePixelY += yStep;
			}
		}

		public void DrawWrapped(
			ISenseHatDisplay display,
			int offsetX,
			int offsetY,
			bool transparent,
			DisplayDirection direction,
			bool flipHorizontal,
			bool flipVertical)
		{
			int spritePixelX0 = _spriteXIndex * 8;
			int spritePixelY0 = _spriteYIndex * 8;

			if (offsetX < 0)
			{
				offsetX = 8 + (offsetX % 8);
			}

			if (offsetY < 0)
			{
				offsetY = 8 + (offsetY % 8);
			}

			bool leftToRight;
			bool topToBottom;
			bool flipAxis;

			PixelSupport.ConvertDirectionParameters(
				direction,
				flipHorizontal,
				flipVertical,
				out leftToRight,
				out topToBottom,
				out flipAxis);

			int xStart;
			int xStep;
			int yStart;
			int yStep;

			if (leftToRight)
			{
				xStart = 0;
				xStep = 1;
			}
			else
			{
				xStart = 7;
				xStep = -1;
				offsetX = 8 - offsetX;
			}

			if (topToBottom)
			{
				yStart = 0;
				yStep = 1;
			}
			else
			{
				yStart = 7;
				yStep = -1;
				offsetY = 8 - offsetY;
			}

			int spritePixelY = yStart;

			for (int screenY = 0; screenY <= 7; screenY++)
			{
				int spritePixelX = xStart;

				for (int screenX = 0; screenX <= 7; screenX++)
				{
					int dX = (8 + spritePixelX - offsetX) % 8;
					int dY = (8 + spritePixelY - offsetY) % 8;

					Color spriteColor = flipAxis
						? _spriteMap.Pixels[spritePixelX0 + dY, spritePixelY0 + dX]
						: _spriteMap.Pixels[spritePixelX0 + dX, spritePixelY0 + dY];

					if (transparent)
					{
						Color screenColor = display.Screen[screenX, screenY];

						spriteColor = GetTransparentPixel(screenColor, spriteColor);
					}

					display.Screen[screenX, screenY] = spriteColor;

					spritePixelX += xStep;
				}

				spritePixelY += yStep;
			}
		}

		private static Color GetTransparentPixel(Color screenColor, Color spriteColor)
		{
			float alpha = spriteColor.A / 255f;

			int mergedR = (int)Math.Round(screenColor.R + spriteColor.R * alpha);
			if (mergedR > 255)
			{
				mergedR = 255;
			}

			int mergedG = (int)Math.Round(screenColor.G + spriteColor.G * alpha);
			if (mergedG > 255)
			{
				mergedG = 255;
			}

			int mergedB = (int)Math.Round(screenColor.B + spriteColor.B * alpha);
			if (mergedB > 255)
			{
				mergedB = 255;
			}

			return Color.FromArgb(255, (byte)mergedR, (byte)mergedG, (byte)mergedB);
		}
	}
}