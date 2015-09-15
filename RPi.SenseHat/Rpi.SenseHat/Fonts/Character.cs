namespace Emmellsoft.IoT.Rpi.SenseHat.Fonts
{
	public abstract class Character
	{
		protected Character(char symbol, int width)
		{
			Symbol = symbol;
			Width = width;
		}

		public char Symbol
		{ get; private set; }

		public int Width
		{ get; }

		public abstract void Render(ISenseHatDisplay display, int offsetX, int offsetY);

		public override string ToString()
		{
			return Symbol.ToString();
		}
	}
}