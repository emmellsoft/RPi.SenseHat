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

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts.SingleColor
{
	/// <summary>
	/// Information regarding a certain pixel of a SingleColorCharacter to be rendered.
	/// </summary>
	public struct SingleColorCharacterRendererPixelMap
	{
		/// <summary>
		/// The character to render.
		/// </summary>
		public SingleColorCharacter Character;

		/// <summary>
		/// The X-coordinate of the pixel in regards to the character (0 .. character width-1).
		/// </summary>
		public int CharPixelX;

		/// <summary>
		/// The Y-coordinate of the pixel in regards to the character (0 .. 7).
		/// </summary>
		public int CharPixelY;

		/// <summary>
		/// The X-coordinate of the display of where to draw the pixel (0 .. 7).
		/// </summary>
		public int DisplayPixelX;

		/// <summary>
		/// The Y-coordinate of the display of where to draw the pixel (0 .. 7).
		/// </summary>
		public int DisplayPixelY;

		/// <summary>
		/// The horizontal offset of the character compared to the left edge of the screen.
		/// </summary>
		public int DisplayOffsetX;

		/// <summary>
		/// The vertical offset of the character compared to the upper edge of the screen.
		/// </summary>
		public int DisplayOffsetY;
	}
}