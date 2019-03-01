namespace Emmellsoft.IoT.RPi.SenseHat.Tools
{
    public static class ColorExtension
    {
        public static Color ToSenseColor(this System.Drawing.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
