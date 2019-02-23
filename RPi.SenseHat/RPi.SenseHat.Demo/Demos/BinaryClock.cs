////////////////////////////////////////////////////////////////////////////
// The binary clock demo is developed by Mark Muller using
// the RPi.SenseHat Windows IoT class library for the Raspberry Pi
////////////////////////////////////////////////////////////////////////////
using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using Color = Emmellsoft.IoT.Rpi.SenseHat.Color;
using Colors = Windows.UI.Colors;

namespace RPi.SenseHat.Demo.Demos
{
    public class BinaryClock : SenseHatDemo
    {
        private readonly Color _activeBitColor = Colors.Red.ToSenseColor();
        private readonly Color _inctiveBitColor = Colors.Gray.ToSenseColor();

        public BinaryClock(ISenseHat senseHat, Action<string> setScreenText)
            : base(senseHat, setScreenText)
        {
        }

        public override void Run()
        {
            while (true)
            {
                SenseHat.Display.Clear();
                SenseHat.Display.Screen[0, 0] = _activeBitColor; // Place a dot to mark the top left corner.

                DateTime now = DateTime.Now;

                DrawBinary(0, now.Hour);
                DrawBinary(3, now.Minute);
                DrawBinary(6, now.Second);

                SenseHat.Display.Update(); // Update the physical display.

                SetScreenText?.Invoke(now.ToString("HH':'mm':'ss")); // Update the MainPage (if it's utilized; i.e. not null).

                // Take a short nap.
                Sleep(TimeSpan.FromMilliseconds(200));
            }
        }

        private void DrawBinary(int x, int value)
        {
            for (int y = 7; y >= 0; y--)
            {
                Color bitColor = (value % 2 == 1)
                    ? _activeBitColor
                    : _inctiveBitColor;

                SenseHat.Display.Screen[x, y] = bitColor;
                SenseHat.Display.Screen[x + 1, y] = bitColor;

                value = value >> 1;
            }
        }
    }
}
