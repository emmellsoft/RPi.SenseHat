using System.Collections.Generic;
using System.Linq;

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts
{
	public class TextScroller
	{
		private readonly ISenseHatDisplay _display;
		private readonly Character[] _chars;
		private int _charIndex;
		private int _charX;
		private int _initialSpace;

		public TextScroller(ISenseHatDisplay display, IEnumerable<Character> characters)
		{
			_display = display;
			_chars = characters.ToArray();
			Reset();
		}

		public void Reset()
		{
			_initialSpace = 8;
			_charIndex = 0;
			_charX = 0;
		}

		public bool Step()
		{
			if (_initialSpace > 0)
			{
				_initialSpace--;
				return true;
			}

			if (_charIndex >= _chars.Length)
			{
				return false;
			}

			_charX++;

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
				Character character = _chars[charIndex];
				character.Render(_display, x, 0);
				x += character.Width;
				charIndex++;
			}
			while ((x < 8) && (charIndex < _chars.Length));
		}
	}
}