////////////////////////////////////////////////////////////////////////////
//
//  This file is part of RPi.SenseHat.Demo
//
//  Copyright (c) 2019, Mattias Larsson
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of
//  this software and associated documentation files (the "Software"), to deal in
//  the Software without restriction, including without limitation the rights to use,
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the
//  Software, and to permit persons to whom the Software is furnished to do so,
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using Emmellsoft.IoT.RPi.SenseHat;
using Emmellsoft.IoT.RPi.SenseHat.Sprites;
using System;

namespace Emmellsoft.IoT.RPi.SenseHat.Demo.Common.Demos
{
    /// <summary>
    /// Use the joystick to move Mario. The middle button switches orientation and flipping of the drawing.
    /// </summary>
    public class SpriteAnimation : SenseHatDemo
    {
        public SpriteAnimation(ISenseHat senseHat)
            : base(senseHat)
        {
        }

        public override void Run()
        {
            // A big thanks to Johan Vinet, pixel artist and animator, for the Mario animation! :-)
            // http://johanvinet.tumblr.com/
            // Twitter handle: @johanvinet

            // Get the pixels of the animation frames.
#if WINDOWS_UWP
            Image image = ImageSupport.GetImage(new Uri("ms-appx:///Assets/MiniMario.png"));
#else

            #region Serialized Image

            // The image below is serialized using the ImageSerializer.Serialize() method.
            // Please see the "SerializeImages" method in the RPi.SenseHat.Tools project.
            const string serializedImage =
                "ATAAAAAIAAAAAP///wD///8A//////8ATf//AE3//wBN///x6AD///8A////AP///wD//////wBN//8ATf//8ej//wBNAP///wD/" +
                "//8A////AP//////AE3//wBN///x6P//AE0A////AP///wD///8A//////8ATf//AE3//wBN///x6AD///8A////AP///wD/////" +
                "/wBN//8ATf//AE3//wBNAP///wD///8A////AP//////AE3//wBN//8ATf//AE0A////AP///wD///8A//////8ATf//AE3//wBN" +
                "//8ATf//AE0A////AP///wD//////wBN//8ATf//AE3//wBN//8ATQD///8A////AP//////AE3//wBN//8ATf//AE3//wBNAP//" +
                "/wD///8A//////8ATf//AE3//wBN//8ATf//AE0A////AP///wD//////wBN//8ATf//AE3//wBN//8ATQD///8A////AP//////" +
                "AE3//wBN//8ATf//AE3//wBNAP///wD/////q1I2///Mqv+rUjb/AAAA///MqgD///8A////AP//////zKr/q1I2/wAAAP//zKoA" +
                "////AP///wD///8A///////Mqv+rUjb/AAAA///MqgD///8A////AP///wD/////q1I2///Mqv+rUjb/AAAA///MqgD///8A////" +
                "AP////+rUjb/q1I2///Mqv+rUjb/AAAAAP///wD///8A/////6tSNv+rUjb//8yq/6tSNv8AAAAA////AP///wD/////q1I2///M" +
                "qv//zKr/q1I2/6tSNv//zKoA////AP//////zKr//8yq/6tSNv+rUjb//8yq/6tSNgD///8A///////Mqv//zKr/q1I2/6tSNv//" +
                "zKr/q1I2AP///wD/////q1I2///Mqv//zKr/q1I2/6tSNv//zKoA////AP////+rUjb/q1I2///Mqv//zKr/q1I2/6tSNgD///8A" +
                "/////6tSNv+rUjb//8yq///Mqv+rUjb/q1I2AP///wD///8A/////6tSNv//zKr//8yq///MqgD///8A////AP///wD//////8yq" +
                "///Mqv//zKr//8yqAP///wD//////wBN//8ATf//zKr//8yq///Mqv//zKoA////AP///wD///8A/////6tSNv//zKr//8yq///M" +
                "qgD///8A////AP///wD/////q1I2/6tSNv//zKr//8yqAP///wD///8A////AP////+rUjb/q1I2///Mqv//zKoA////AP///wD/" +
                "/////wBN//8ATf///yf/Ka3//x0rUwD///8A//////8ATf//AE3///8n/ymt//8prf///6MAAP//////8egA//////8ATf///yf/" +
                "Ka3//ymt////owD///HoAP///wD//////wBN//8ATf///yf/Ka3//x0rUwD///8A////AP////8prf///wBN//8ATf///yf/HStT" +
                "AP///wD///8A/////ymt////AE3//wBN////J/8dK1P///HoAP///wD//////wBN///x6P8prf//Ka3//x0rUwD///////HoAP//" +
                "//8prf//Ka3//ymt//8prf//HStT/4N2nAD/////fiVT/ymt//8prf//Ka3//ymt//8dK1MA////AP///wD//////wBN///x6P8p" +
                "rf//Ka3//x0rUwD///8A////AP////8prf//Ka3///8ATf//AE3///HoAP///wD/////q1I2/ymt//8prf//Ka3///8ATf//AE0A" +
                "////AP///wD///8A/////6tSNv+rUjb/fiVTAP///wD///8A////AP////9+JVMA////AP////+rUjYA////AP///wD///8A////" +
                "AP///wD///8A////AP////+rUjYA////AP///wD///8A/////6tSNv+rUjb/fiVTAP///wD///8A////AP////+rUjYA////AP//" +
                "//9+JVMA////AP///wD///8A////AP///wD///8A////AP////9+JVMA////";

            #endregion Serialized Image

            Image image = ImageSerializer.Deserialize(serializedImage);
#endif

            // Create a sprite map from the pixels.
            var spriteMap = new SpriteMap(image);

            // Keep track of the animation frame...
            int animationIndex = 0;
            Sprite sprite = spriteMap.GetSprite(animationIndex);

            // ...and when it's time to update it.
            TimeSpan frameDuration = TimeSpan.FromMilliseconds(70);
            DateTime nextAnimationUpdateTime = DateTime.Now.Add(frameDuration);

            // Keep track of the location and orientation of the sprite.
            int spriteX = 0;
            int spriteY = 0;
            DisplayDirection direction = DisplayDirection.Deg0;
            bool flipHorizontal = false;
            bool flipVertical = false;

            while (true)
            {
                bool redrawSprite = false;

                //Is it time to update the animation ?
                if (DateTime.Now >= nextAnimationUpdateTime)
                {
                    // Yes. The next time to update is:
                    nextAnimationUpdateTime = DateTime.Now.Add(frameDuration);

                    // Needs to redraw the sprite.
                    redrawSprite = true;

                    // Select the next sprite index.
                    animationIndex++;
                    if (animationIndex >= 6)
                    {
                        animationIndex = 0;
                    }

                    // Pick out the sprite.
                    sprite = spriteMap.GetSprite(animationIndex);
                }

                if (SenseHat.Joystick.Update()) // Has any of the buttons on the joystick changed?
                {
                    UpdatePosition(ref spriteX, ref spriteY); // Move the sprite.

                    UpdateDrawingDirection(ref direction, ref flipHorizontal, ref flipVertical); // Re-orientate the sprite.

                    // Needs to redraw the sprite.
                    redrawSprite = true;
                }

                if (redrawSprite)
                {
                    SenseHat.Display.Clear(); // Clear the screen.

                    // Draw the sprite.
                    sprite.Draw(SenseHat.Display, spriteX, spriteY, true, direction, flipHorizontal, flipVertical);

                    SenseHat.Display.Update(); // Update the physical display.
                }

                // Take a short nap.
                Sleep(TimeSpan.FromMilliseconds(2));
            }
        }

        private void UpdatePosition(ref int spriteX, ref int spriteY)
        {
            if (SenseHat.Joystick.LeftKey == KeyState.Pressed)
            {
                if (spriteX > -8)
                {
                    spriteX--;
                }
            }
            else if (SenseHat.Joystick.RightKey == KeyState.Pressed)
            {
                if (spriteX < 8)
                {
                    spriteX++;
                }
            }

            if (SenseHat.Joystick.UpKey == KeyState.Pressed)
            {
                if (spriteY > -8)
                {
                    spriteY--;
                }
            }
            else if (SenseHat.Joystick.DownKey == KeyState.Pressed)
            {
                if (spriteY < 8)
                {
                    spriteY++;
                }
            }
        }

        private void UpdateDrawingDirection(ref DisplayDirection direction, ref bool flipHorizontal, ref bool flipVertical)
        {
            if (SenseHat.Joystick.EnterKey == KeyState.Pressed)
            {
                direction++;
                if (direction > DisplayDirection.Deg270)
                {
                    direction = DisplayDirection.Deg0;

                    flipHorizontal = !flipHorizontal;

                    if (!flipHorizontal)
                    {
                        flipVertical = !flipVertical;
                    }
                }
            }
        }
    }
}
