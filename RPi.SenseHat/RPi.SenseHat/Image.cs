namespace Emmellsoft.IoT.RPi.SenseHat
{
    public class Image
    {
        public Image(int width, int height)
        {
            Width = width;
            Height = height;
            Length = width * height;
            Pixels = new Color[width, height];
        }

        public Color this[int x, int y]
        {
            get => Pixels[x, y];
            set => Pixels[x, y] = value;
        }

        public int Width { get; }

        public int Height { get; }

        public int Length { get; }

        public Color[,] Pixels { get; }

        public void Clear()
        {
            Fill(Color.FromArgb(0, 0, 0, 0));
        }

        public void Fill(Color color)
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Pixels[x, y] = color;
                }
            }
        }
    }
}
