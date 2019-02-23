using Emmellsoft.IoT.Rpi.SenseHat;

namespace RPi.SenseHat.Demo
{
    public static class ColorExtension
    {
        public static Color ToSenseColor(this Windows.UI.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}
