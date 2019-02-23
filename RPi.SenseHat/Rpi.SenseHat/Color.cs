namespace Emmellsoft.IoT.Rpi.SenseHat
{
    public struct Color
    {
        private Color(byte a, byte r, byte g, byte b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }

        public byte A { get; }

        public byte B { get; }

        public byte G { get; }

        public byte R { get; }

        public static bool operator ==(Color c1, Color c2)
        {
            return (c1.A == c2.A) &&
                   (c1.R == c2.R) &&
                   (c1.G == c2.G) &&
                   (c1.B == c2.B);
        }

        public static bool operator !=(Color c1, Color c2)
        {
            return (c1.A != c2.A) ||
                   (c1.R != c2.R) ||
                   (c1.G != c2.G) ||
                   (c1.B != c2.B);
        }

        public bool Equals(Color other)
        {
            return (A == other.A) &&
                   (R == other.R) &&
                   (G == other.G) &&
                   (B == other.B);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return (obj is Color other) && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = A.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ R.GetHashCode();
                return hashCode;
            }
        }

        public static Color FromRgb(byte r, byte g, byte b)
        {
            return new Color(0xFF, r, g, b);
        }

        public static Color FromRgb(int rgb)
        {
            byte r = (byte)((rgb >> 16) & 0xFF);
            byte g = (byte)((rgb >> 8) & 0xFF);
            byte b = (byte)(rgb & 0xFF);

            return new Color(0xFF, r, g, b);
        }

        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color(a, r, g, b);
        }

        public static Color FromArgb(int argb)
        {
            byte a = (byte)((argb >> 24) & 0xFF);
            byte r = (byte)((argb >> 16) & 0xFF);
            byte g = (byte)((argb >> 8) & 0xFF);
            byte b = (byte)(argb & 0xFF);

            return new Color(a, r, g, b);
        }

        public int ToArgb()
        {
            return (A << 24) | (R << 16) | (G << 8) | B;
        }
    }
}
