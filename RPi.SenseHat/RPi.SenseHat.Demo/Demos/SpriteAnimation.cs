////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Demo
//
//  Copyright (c) 2016, Mattias Larsson
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

using System;
using System.Linq;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Sprites;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// Use the joystick to move Mario. The middle button switches orientation and flipping of the drawing.
	/// </summary>
	public class SpriteAnimation : SenseHatDemo
	{
		public SpriteAnimation(ISenseHat senseHat, MainPage mainPage)
			: base(senseHat, mainPage)
		{
		}

		public override void Run()
		{
			// A big thanks to Johan Vinet, pixel artist and animator, for the Mario animation! :-)
			// http://johanvinet.tumblr.com/
			// Twitter handle: @johanvinet
			var imageUri = new Uri("ms-appx:///Assets/MiniMario.png");

			// Get the pixels of the animation frames.
			Color[,] pixels = PixelSupport.GetPixels(imageUri).Result;

			// Create a sprite map from the pixels.
			var spriteMap = new SpriteMap(pixels);

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