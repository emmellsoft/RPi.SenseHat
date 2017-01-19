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
using System.Collections.Generic;

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts
{
	/// <summary>
	/// The base class for Font implementations.
	/// </summary>
	/// <typeparam name="TChar"></typeparam>
	public abstract class Font<TChar> where TChar : Character
	{
		private const char FallbackChar = '?';
		private readonly Dictionary<char, TChar> _charDict = new Dictionary<char, TChar>();
		private TChar _fallbackCharacter;

		protected Font(IEnumerable<TChar> chars)
		{
			InitDictionary(chars);
		}

		private void InitDictionary(IEnumerable<TChar> chars)
		{
			foreach (TChar c in chars)
			{
				if (_charDict.ContainsKey(c.Symbol))
				{
					throw new ArgumentException("Duplicate symbol: " + c.Symbol);
				}

				_charDict.Add(c.Symbol, c);
			}

			TChar fallbackCharacter;
			if (_charDict.TryGetValue(FallbackChar, out fallbackCharacter))
			{
				_fallbackCharacter = fallbackCharacter;
			}
			else
			{
				throw new ArgumentException("Missing fallback-symbol: " + FallbackChar);
			}
		}

		/// <summary>
		/// Get all available characters.
		/// </summary>
		/// <returns></returns>
		public IEnumerable<TChar> GetChars()
		{
			return _charDict.Values;
		}

		/// <summary>
		/// Converts a text string into a set of character objects.
		/// </summary>
		/// <param name="text">The text string to convert into font characters.</param>
		public IEnumerable<TChar> GetChars(string text)
		{
			foreach (char symbol in text ?? string.Empty)
			{
				TChar c;
				if (!_charDict.TryGetValue(symbol, out c))
				{
					c = _fallbackCharacter;
				}

				yield return c;
			}
		}
	}
}