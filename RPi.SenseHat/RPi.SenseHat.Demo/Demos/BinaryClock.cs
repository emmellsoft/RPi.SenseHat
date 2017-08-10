////////////////////////////////////////////////////////////////////////////
// The binary clock demo is developed by Mark Muller using
// the RPi.SenseHat Windows IoT class library for the Raspberry Pi 
////////////////////////////////////////////////////////////////////////////
using Emmellsoft.IoT.Rpi.SenseHat;
using System;
using Windows.UI;

namespace RPi.SenseHat.Demo.Demos
{
    public class BinaryClock : SenseHatDemo
    {
        private readonly Color[] _colors = { Colors.Red, Colors.Green, Colors.Blue, Colors.Cyan, Colors.Magenta, Colors.Yellow, Colors.White };
        private int _colorIndex = 0;

        public BinaryClock(ISenseHat senseHat, Action<string> setScreenText) 
            : base(senseHat, setScreenText)
        {
        }

        public override void Run()
        {
            DateTime dt;
            int hours;
            int minutes;
            int seconds;

            SenseHat.Display.Clear();

            while (true)
            {
                SenseHat.Display.Clear();
                SenseHat.Display.Screen[0, 0] = _colors[2];// find corner = top left

                dt = DateTime.Now;
                hours = dt.Hour;
                minutes = dt.Minute;
                seconds = dt.Second;

                DrawBinary(0, hours);
                DrawBinary(3, minutes);
                DrawBinary(6,seconds);

                SenseHat.Display.Update(); // Update the physical display.

                SetScreenText?.Invoke($"{hours}:{minutes}:{seconds}"); // Update the MainPage (if it's utilized; i.e. not null).

                // Take a short nap.
                Sleep(TimeSpan.FromMilliseconds(200));
            }
        }

        private void DrawBinary(int x, int value)
        {
            for(int y=7; y>=0 && value!=0; y--)
            {
                if (value % 2 == 1)
                {
                    SenseHat.Display.Screen[x,y] = _colors[_colorIndex];
                    SenseHat.Display.Screen[x+1, y] = _colors[_colorIndex];
                }
                value=value >> 1;
            }

        }

    }
}
