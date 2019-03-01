using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Emmellsoft.IoT.RPi.SenseHat;
using Emmellsoft.IoT.RPi.SenseHat.Demo;
using Emmellsoft.IoT.RPi.SenseHat.Demo.Common.Demos;

namespace RPi.SenseHat.Demo.Core
{
    
    internal class Program
    {

        private static Action<string> setScreenText = (str) => { };
        private static Dictionary<string, Tuple<SenseHatDemo,string>> demoDictionary ;

        private static void Main(string[] args)
        {
            CoreDeviceFactory.Init();
            ISenseHat senseHat = SenseHatFactory.GetSenseHat();
            demoDictionary = new Dictionary<string, Tuple<SenseHatDemo,string>>()
            {
                {"1" , new Tuple<SenseHatDemo,string>(new DiscoLights(senseHat), "Click on the joystick to change drawing mode!")},
                {"2" , new Tuple<SenseHatDemo,string>(new JoystickPixel(senseHat, setScreenText), "Use the joystick to move the pixel around.")},
                {"3" , new Tuple<SenseHatDemo,string>(new WriteTemperature(senseHat, setScreenText), "Is it only me or does it show some unusual high temperature? :-S")},
                {"4" , new Tuple<SenseHatDemo,string>(new GravityBlob(senseHat, setScreenText), "The green blob is drawn to the center of the earth! If you hold it upside down it gets angry and turns red. :-O")},
                {"5" , new Tuple<SenseHatDemo,string>(new Compass(senseHat, setScreenText), "Note! You must calibrate the magnetic sensor by moving the Raspberry Pi device around in an 'eight' figure a few seconds at startup!")},
                {"6" , new Tuple<SenseHatDemo,string>(new SingleColorScrollText(senseHat, "Hello Raspberry Pi 3 Sense HAT!"), "Click on the joystick to change drawing mode!")},
                {"7" , new Tuple<SenseHatDemo,string>(new MultiColorScrollText(senseHat, "Hello Raspberry Pi 3 Sense HAT!"), "")},
                {"8" , new Tuple<SenseHatDemo,string>(new SpriteAnimation(senseHat), " Use the joystick to move Mario. The middle button switches orientation and flipping of the drawing.")},
                {"9" , new Tuple<SenseHatDemo,string>(new GammaTest(senseHat), "Tries out different gamma settings for the LED display. Use the joystick to play around.")},
                {"10", new Tuple<SenseHatDemo,string>(new ReadAllSensors(senseHat, setScreenText), "Shows an example of how to read all the different sensors.")},
                {"11", new Tuple<SenseHatDemo,string>(new BinaryClock(senseHat, setScreenText), "Shows a binary clock (by Mark Muller)")}
            };
            
            RunInteractive(senseHat);
            //await RunNonInteractive(senseHat);
        }

        private static void RunInteractive(ISenseHat senseHat)
        {
            Console.WriteLine("SELECT 1 of the following:");
            Console.WriteLine("1. DiscoLights");
            Console.WriteLine("2. JoystickPixel");
            Console.WriteLine("3.WriteTemperature");
            Console.WriteLine("4.GravityBlob");
            Console.WriteLine("5.Compass"); 
            Console.WriteLine("6.SingleColorScrollText");
            Console.WriteLine("7.MultiColorScrollText");
            Console.WriteLine("8.SpriteAnimation");
            Console.WriteLine("9.GammaTest");
            Console.WriteLine("10.ReadAllSensors");
            Console.WriteLine("11.BinaryClock");
            Console.WriteLine("*****************************");
            string selected = Console.ReadLine();
            
            Console.WriteLine(demoDictionary[selected].Item2);
            demoDictionary[selected].Item1.Run();
                 
        }
    }
}
