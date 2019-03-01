using Emmellsoft.IoT.RPi.SenseHat.Fonts.MultiColor;
using Emmellsoft.IoT.RPi.SenseHat.Fonts.SingleColor;
using Emmellsoft.IoT.RPi.SenseHat.Tools.Font;
using Emmellsoft.IoT.RPi.SenseHat.Tools.LedBuffer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Emmellsoft.IoT.RPi.SenseHat.Tools
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ColorFontWork();
            LedBufferWork();
            SingleColorFontWork();
            SingleColorTinyFontWork();
            ValidateImageSerializer();
            SerializeImages();
        }

        private static bool ImagesEqual(Image a, Image b, bool ignoreAlpha)
        {
            if (a.Width != b.Width)
            {
                return false;
            }

            if (a.Height != b.Height)
            {
                return false;
            }

            for (int y = 0; y < a.Height; y++)
            {
                for (int x = 0; x < a.Width; x++)
                {
                    if (ignoreAlpha)
                    {
                        if (a[x, y].R != b[x, y].R)
                        {
                            return false;
                        }

                        if (a[x, y].G != b[x, y].G)
                        {
                            return false;
                        }

                        if (a[x, y].B != b[x, y].B)
                        {
                            return false;
                        }
                    }
                    else if (a[x, y] != b[x, y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void ColorFontWork()
        {
            const string symbols = " ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖÉÜabcdefghijklmnopqrstuvwxyzåäöéü0123456789.,?!\"#$%&-+*:;/\\<>()'`=";

            Image image = LoadUwpAssetImage("ColorFont.png");
            MultiColorFont font = MultiColorFont.LoadFromImage(image, symbols, System.Drawing.Color.Transparent.ToSenseColor());
            var chars = font.GetChars().ToArray();
            var widths = chars.Select(c => c.Width).ToArray();
        }

        private static Image LoadUwpAssetImage(string fileName)
        {
            const string relativeAssetPath = @"..\..\..\RPi.SenseHat.Demo.UWP\Assets";

            var dir = Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
            var path = Path.GetFullPath(Path.Combine(dir ?? string.Empty, relativeAssetPath, fileName));

            return ImageSupport.GetImage(path);
        }

        private static void SingleColorFontWork()
        {
            var bitmap = new Bitmap(@"Font\BWFont.png");
            const string chars = " ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖÉÜabcdefghijklmnopqrstuvwxyzåäöéü0123456789.,?!\"#$%&-+*:;/\\<>()'`=";

            SingleColorFont singleColorFont = SingleColorFontBuilder.GetSingleColorFont(bitmap, chars);
            byte[] fontBytes = singleColorFont.Serialize().ToArray();
            var fontBytesAsCode = ToCSharp(fontBytes);

            Tuple<string, Bitmap> tuple = SingleColorFontBuilder.GetFontBitmap(fontBytes);
            tuple.Item2.Save(@"Font\BWFont_recreated.png");
        }

        private static void SingleColorTinyFontWork()
        {
            var bitmap = new Bitmap(@"Font\TinyBWFont.png");
            const string chars = " 0123456789ABCDEF+-%*=.:!?/\\'";

            SingleColorFont tinyFont = SingleColorFontBuilder.GetSingleColorFont(bitmap, chars);
            byte[] fontBytes = tinyFont.Serialize().ToArray();
            var fontBytesAsCode = ToCSharp(fontBytes);
        }

        private static void LedBufferWork()
        {
            byte[] originalGamma =
            {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x01,
                0x02, 0x02, 0x03, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0E, 0x0F, 0x11,
                0x12, 0x14, 0x15, 0x17, 0x19, 0x1B, 0x1D, 0x1F
            };

            double senseHatGamma = FindBestGammaMatch.Best5BitGammaMatch(originalGamma, 2, 3, 0.01);

            byte[] senseHatGammaTable = GammaCalc.Get5BitGamma(senseHatGamma).ToArray();
            string senseHatGammaTableAsCode = ToCSharp(senseHatGammaTable);

            byte[] senseHatInverseGammaTable = GammaCalc.Get5To8BitInvertedGamma(senseHatGamma).ToArray();
            string senseHatInverseGammaTableAsCode = ToCSharp(senseHatInverseGammaTable);

            var initalPixels = LedBufferSupport.BufferToImage(LedBufferSupport.GetInitialLedBuffer(), senseHatGamma);
            string initalPixelsAsCode = ToCSharp(initalPixels);

            byte[] initialBufferRecreated = LedBufferSupport.ImageToBuffer(initalPixels, senseHatGamma);
            string initialBufferRecreatedAsCode = ToCSharp(initialBufferRecreated);
            string originalInitialBufferAsCode = ToCSharp(LedBufferSupport.GetInitialLedBuffer());

            var xxx = initialBufferRecreatedAsCode + "\r\n" + originalInitialBufferAsCode;

            RotationTest.Test();
        }

        private static string ToCSharp(IEnumerable<byte> bytes)
        {
            return string.Join(", ", bytes.Select(ByteToCSharp));
            //return string.Join(",", bytes.Select(g => g.ToString().PadLeft(3)));
            //return String.Join("  ", bytes.Select(g => g.ToString("x2")));
        }

        private static string ToCSharp(Image image, bool compact = true)
        {
            var text = new StringBuilder();

            if (compact)
            {
                text.AppendLine("Color[] colors = new[]");
                text.AppendLine("{");

                for (int y = 0; y < 8; y++)
                {
                    text.Append("\t");

                    for (int x = 0; x < 8; x++)
                    {
                        int argb = image[x, y].ToArgb();
                        int rgb = argb & 0x00FFFFFF;
                        text.Append("0x" + rgb.ToString("X6"));

                        if ((x < 7) || (y < 7))
                        {
                            text.Append(", ");
                        }
                    }

                    text.AppendLine();
                }

                text.AppendLine("}");
                text.AppendLine(".Select(rgb => Color.FromArgb(0xFF, (byte)((rgb >> 16) & 0xFF), (byte)((rgb >> 8) & 0xFF), (byte)(rgb & 0xFF)))");
                text.AppendLine(".ToArray();");
            }
            else
            {
                text.AppendLine("Color[,] colors = new Color[,]");
                text.AppendLine("{");

                for (int y = 0; y < 8; y++)
                {
                    text.Append("\t{ ");
                    for (int x = 0; x < 8; x++)
                    {
                        Color color = image[x, y];

                        if (x > 0)
                        {
                            text.Append(", ");
                        }

                        text.Append("Color.FromArgb(0xff, ");
                        text.Append(string.Join(", ", new[] { color.R, color.G, color.B }.Select(ByteToCSharp)));
                        text.Append(")");
                    }
                    text.AppendLine(" },");
                }

                text.AppendLine("};");
            }

            return text.ToString();
        }

        private static string ByteToCSharp(byte value)
        {
            return "0x" + value.ToString("X2");
        }

        private static void ValidateImageSerializer()
        {
            Image originalImage = LoadUwpAssetImage("ColorFont.png");

            string serializedImage1 = ImageSerializer.Serialize(originalImage);
            string serializedImage2 = ImageSerializer.Serialize(originalImage, true);
            var deserializedImage1 = ImageSerializer.Deserialize(serializedImage1);
            var deserializedImage2 = ImageSerializer.Deserialize(serializedImage2);

            if (!ImagesEqual(originalImage, deserializedImage1, true) ||
                !ImagesEqual(originalImage, deserializedImage2, false))
            {
                throw new Exception("Image serialized failed.");
            }
        }

        private static void SerializeImages()
        {
            Image colorFontImage = LoadUwpAssetImage("ColorFont.png");
            string colorFontImageSerialized = ImageSerializer.Serialize(colorFontImage);

            Image miniMarioImage = LoadUwpAssetImage("MiniMario.png");
            string miniMarioImageSerialized = ImageSerializer.Serialize(miniMarioImage, true);
        }
    }
}
