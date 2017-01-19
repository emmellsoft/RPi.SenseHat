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

#if NETFX_CORE
using Windows.UI;
#else
using System.Drawing;
#endif

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts.MultiColor
{
	public class MultiColorCharacterRenderer : CharacterRenderer<MultiColorCharacter>
	{
		public override void Render(
			ISenseHatDisplay display,
			MultiColorCharacter character,
			int offsetX,
			int offsetY)
		{
			int charWidth = character.Pixels.GetLength(0);
			int charHeight = character.Pixels.GetLength(1);

			int right = offsetX + charWidth - 1;
			int bottom = offsetY + charHeight - 1;

			if ((offsetY > 7) || (bottom < 0) || (offsetX > 7) || (right < 0))
			{
				return;
			}

			int charPixelXInit = 0;
			int charPixelYInit = 0;

			if (offsetX < 0)
			{
				charPixelXInit -= offsetX;
				offsetX = 0;
			}

			if (offsetY < 0)
			{
				charPixelYInit -= offsetY;
				offsetY = 0;
			}

			if (right > 7)
			{
				right = 7;
			}

			if (bottom > 7)
			{
				bottom = 7;
			}

			int charPixelY = charPixelYInit;

			for (int screenY = offsetY; screenY <= bottom; screenY++)
			{
				int charPixelX = charPixelXInit;

				for (int screenX = offsetX; screenX <= right; screenX++)
				{
					Color charColor = character.Pixels[charPixelX, charPixelY];

					if (charColor != character.TransparencyColor)
					{
						display.Screen[screenX, screenY] = charColor;
					}

					charPixelX++;
				}

				charPixelY++;
			}
		}
	}
}