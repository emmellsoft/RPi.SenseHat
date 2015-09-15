#if NETFX_CORE
using Windows.UI;
#else
using System.Drawing;
#endif
using System;

namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts
{
	public struct PixelMap
	{
		public BwCharacter Character;
		public int CharPixelX;
		public int CharPixelY;
		public int DisplayOffsetX;
		public int DisplayOffsetY;
	}

	public class BwCharacter : Character
	{
		public static readonly Func<PixelMap, Color> GetRedColor = pixelMap =>
#if NETFX_CORE
			Colors.Red;
#else
			Color.Red;
#endif

		public BwCharacter(char symbol, byte[] columns)
			: base(symbol, columns.Length)
		{
			Columns = columns;
			GetColor = GetRedColor;
		}

		public byte[] Columns
		{ get; }

		public Func<PixelMap, Color> GetColor
		{ get; set; }

		public override void Render(ISenseHatDisplay display, int offsetX, int offsetY)
		{
			int right = offsetX + Columns.Length - 1;
			int bottom = offsetY + 7;

			if ((offsetY > 7) || (bottom < 0) || (offsetX > 7) || (right < 0))
			{
				return;
			}

			int columnIndex = 0;
			int maskInit = 1;
			var pixelMap = new PixelMap
			{
				Character = this,
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

			for (int x = offsetX; x <= right; x++)
			{
				int mask = maskInit;

				byte column = Columns[columnIndex++];

				pixelMap.CharPixelY = charPixelYInit;

				for (int y = offsetY; y <= bottom; y++)
				{
					if ((column & mask) == mask)
					{
						display.Screen[x, y] = GetColor(pixelMap);
					}

					mask <<= 1;
					pixelMap.CharPixelY++;
				}

				pixelMap.CharPixelX++;
			}
		}
	}
}