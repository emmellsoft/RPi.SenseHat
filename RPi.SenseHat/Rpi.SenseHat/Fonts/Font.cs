using System;
using System.Collections.Generic;

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts
{
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

		public IEnumerable<TChar> GetChars()
		{
			return _charDict.Values;
		}

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