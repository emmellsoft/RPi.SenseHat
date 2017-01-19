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

using System.Collections.Generic;
using System.Linq;

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts
{
	/// <summary>
	/// A simple text scroller.
	/// </summary>
	public class TextScroller<TChar> where TChar : Character
	{
		private readonly ISenseHatDisplay _display;
		private readonly CharacterRenderer<TChar> _characterRenderer;
		private readonly TChar[] _chars;
		private int _charIndex;
		private int _charX;
		private int _initialSpace;

		/// <summary>
		/// Constructor of the scroller.
		/// </summary>
		/// <param name="display">The display to render at.</param>
		/// <param name="characterRenderer">The character renderer.</param>
		/// <param name="characters">The characters to scroll.</param>
		public TextScroller(
			ISenseHatDisplay display,
			CharacterRenderer<TChar> characterRenderer,
			IEnumerable<TChar> characters)
		{
			_display = display;
			_characterRenderer = characterRenderer;
			_chars = characters.ToArray();

			Reset();
		}

		/// <summary>
		/// The total number of pixels scrolled by.
		/// </summary>
		public int ScrollPixelOffset
		{ get; private set; }

		/// <summary>
		/// Make the scroller start over.
		/// </summary>
		public void Reset()
		{
			_initialSpace = 8;
			_charIndex = 0;
			_charX = 0;
			ScrollPixelOffset = 0;
		}

		/// <summary>
		/// Scroll one pixel. Returns true if scrolled and false if the scroll is completed.
		/// </summary>
		public bool Step()
		{
			if (_initialSpace > 0)
			{
				ScrollPixelOffset++;
				_initialSpace--;
				return true;
			}

			if (_charIndex >= _chars.Length)
			{
				return false;
			}

			_charX++;
			ScrollPixelOffset++;

			if (_charX < _chars[_charIndex].Width)
			{
				return true;
			}

			_charIndex++;

			if (_charIndex >= _chars.Length)
			{
				return false;
			}

			_charX = 0;
			return true;
		}

		/// <summary>
		/// Renders the scroll to the display.
		/// </summary>
		public void Render()
		{
			if (_charIndex >= _chars.Length)
			{
				return;
			}

			int x = _initialSpace - _charX;
			int charIndex = _charIndex;

			do
			{
				TChar character = _chars[charIndex];
				_characterRenderer.Render(_display, character, x, 0);
				x += character.Width;
				charIndex++;
			}
			while ((x < 8) && (charIndex < _chars.Length));
		}
	}
}