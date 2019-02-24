using System;
using System.Globalization;
using System.Text;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
    public static class ImageSerializer
    {
        private const char PartSeparator = ';';
        private const char ColorSeparator = ',';

        public static string Serialize1(Image image)
        {
            var serialized = new StringBuilder();
            serialized.Append(image.Width);
            serialized.Append(PartSeparator);
            serialized.Append(image.Height);
            for (int y = 0; y < image.Height; y++)
            {
                serialized.Append(PartSeparator);
                for (int x = 0; x < image.Width; x++)
                {
                    if (x > 0)
                    {
                        serialized.Append(ColorSeparator);
                    }

                    serialized.Append(image[x, y].ToArgb().ToString("x8"));
                }
            }

            return serialized.ToString();
        }

        public static string Serialize(Image image, bool includeAlpha = false)
        {
            int colorSize = includeAlpha ? 4 : 3;
            int totalSize = image.Length * colorSize + 9;
            byte[] buffer = new byte[totalSize];

            int index = 0;

            buffer[index++] = (byte)(includeAlpha ? 1 : 0);
            InsertInt(ref index, image.Width);
            InsertInt(ref index, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    InsertColor(ref index, image[x, y]);
                }
            }

            void InsertInt(ref int offset, int value)
            {
                byte[] valueBytes = BitConverter.GetBytes(value);
                Array.Copy(valueBytes, 0, buffer, offset, 4);
                offset += 4;
            }

            void InsertColor(ref int offset, Color color)
            {
                if (includeAlpha)
                {
                    buffer[offset++] = color.A;
                }

                buffer[offset++] = color.R;
                buffer[offset++] = color.G;
                buffer[offset++] = color.B;
            }

            return Convert.ToBase64String(buffer);
        }

        public static Image Deserialize1(string serialized)
        {
            string[] parts = serialized.Split(PartSeparator);
            if (parts.Length < 2)
            {
                throw new FormatException("Illegal number of parts");
            }

            int width = int.Parse(parts[0]);
            int height = int.Parse(parts[1]);

            if (parts.Length != height + 2)
            {
                throw new FormatException("Illegal number of rows");
            }

            var image = new Image(width, height);

            for (int y = 0; y < image.Height; y++)
            {
                string[] colors = parts[y + 2].Split(ColorSeparator);
                if (colors.Length != width)
                {
                    throw new FormatException("Illegal number of columns");
                }

                for (int x = 0; x < image.Width; x++)
                {
                    image[x, y] = Color.FromArgb(int.Parse(colors[x], NumberStyles.HexNumber));
                }
            }

            return image;
        }

        public static Image Deserialize(string serialized)
        {
            byte[] buffer = Convert.FromBase64String(serialized);
            if (buffer.Length < 9)
            {
                throw new FormatException("Illegal buffer size");
            }

            int index = 0;

            bool includeAlpha = buffer[index++] == 1;
            int colorSize = includeAlpha ? 4 : 3;

            int width = ExtractInt(ref index);
            int height = ExtractInt(ref index);

            int totalSize = width * height * colorSize + 9;
            if (buffer.Length < totalSize)
            {
                throw new FormatException("Illegal buffer size");
            }

            var image = new Image(width, height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    image[x, y] = ExtractColor(ref index);
                }
            }

            int ExtractInt(ref int offset)
            {
                int value = BitConverter.ToInt32(buffer, offset);
                offset += 4;
                return value;
            }

            Color ExtractColor(ref int offset)
            {
                byte a = includeAlpha ? buffer[offset++] : (byte)0xFF;
                byte r = buffer[offset++];
                byte g = buffer[offset++];
                byte b = buffer[offset++];
                return Color.FromArgb(a, r, g, b);
            }

            return image;
        }
    }
}
