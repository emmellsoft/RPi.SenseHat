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
#if NETFX_CORE
using Windows.UI;
#else
using System.Drawing;
#endif

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor
{
	/// <summary>
	/// Renderer of a SingleColorCharacter.
	/// </summary>
	public class SingleColorCharacterRenderer : CharacterRenderer<SingleColorCharacter>
	{
		private readonly Func<SingleColorCharacterRendererPixelMap, Color> _getColor;

		/// <summary>
		/// The constructor.
		/// </summary>
		/// <param name="getColor">Function that finds the color of a certain pixel to draw.</param>
		public SingleColorCharacterRenderer(Func<SingleColorCharacterRendererPixelMap, Color> getColor)
		{
			_getColor = getColor;
		}

		/// <summary>
		/// Renders a character on the display at the specified offset.
		/// </summary>
		/// <param name="display">The display to render to.</param>
		/// <param name="character">The character to render.</param>
		/// <param name="offsetX">The X-position of the left-most edge of the character.</param>
		/// <param name="offsetY">The Y-position of the top-most edge of the character.</param>
		public override void Render(ISenseHatDisplay display, SingleColorCharacter character, int offsetX, int offsetY)
		{
			int right = offsetX + character.Columns.Length - 1;
			int bottom = offsetY + 7;

			if ((offsetY > 7) || (bottom < 0) || (offsetX > 7) || (right < 0))
			{
				return;
			}

			int columnIndex = 0;
			int maskInit = 1;
			var pixelMap = new SingleColorCharacterRendererPixelMap
			{
				Character = character,
				DisplayOffsetX = offsetX,
				DisplayOffsetY = offsetY
			};

			int charPixelYInit = 0;

			if (offsetX < 0)
			{
				columnIndex -= offsetX;
				pixelMap.CharPixelX -= offsetX;
				offsetX = 0;
			}

			if (offsetY < 0)
			{
				maskInit <<= -offsetY;
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

			pixelMap.DisplayPixelX = 0;
			for (int screenX = offsetX; screenX <= right; screenX++)
			{
				int mask = maskInit;

				byte column = character.Columns[columnIndex++];

				pixelMap.CharPixelY = charPixelYInit;
				pixelMap.DisplayPixelY = 0;

				for (int screenY = offsetY; screenY <= bottom; screenY++)
				{
					if ((column & mask) == mask)
					{
						display.Screen[screenX, screenY] = _getColor(pixelMap);
					}

					mask <<= 1;
					pixelMap.CharPixelY++;
					pixelMap.DisplayPixelY++;
				}

				pixelMap.CharPixelX++;
				pixelMap.DisplayPixelX++;
			}
		}
	}
}