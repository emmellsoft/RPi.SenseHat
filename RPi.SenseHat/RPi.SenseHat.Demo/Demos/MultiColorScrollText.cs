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
using System.Collections.Generic;
using Windows.UI;
using Emmellsoft.IoT.Rpi.SenseHat;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts;
using Emmellsoft.IoT.Rpi.SenseHat.Fonts.MultiColor;

namespace RPi.SenseHat.Demo.Demos
{
	/// <summary>
	/// Multi-color scroll-text.
	/// </summary>
	public class MultiColorScrollText : SenseHatDemo
	{
		private readonly string _scrollText;

		public MultiColorScrollText(ISenseHat senseHat, MainPage mainPage, string scrollText)
			: base(senseHat, mainPage)
		{
			_scrollText = scrollText;
		}

		public override void Run()
		{
			// Create the font from the image.
			MultiColorFont font = MultiColorFont.LoadFromImage(
				new Uri("ms-appx:///Assets/ColorFont.png"),
				" ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖÉÜabcdefghijklmnopqrstuvwxyzåäöéü0123456789.,?!\"#$%&-+*:;/\\<>()'`=",
				Color.FromArgb(0xFF, 0xFF, 0x00, 0xFF)).Result;

			// Get the characters to scroll.
			IEnumerable<MultiColorCharacter> characters = font.GetChars(_scrollText);

			// Choose a background color (or draw your own more complex background!)
			Color backgroundColor = Color.FromArgb(0xFF, 0x00, 0x20, 0x00);

			// Create the character renderer.
			var characterRenderer = new MultiColorCharacterRenderer();

			// Create the text scroller.
			var textScroller = new TextScroller<MultiColorCharacter>(
				SenseHat.Display,
				characterRenderer,
				characters);

			while (true)
			{
				// Step the scroller.
				if (!textScroller.Step())
				{
					// Reset the scroller when reaching the end.
					textScroller.Reset();
				}

				// Clear the display.
				SenseHat.Display.Fill(backgroundColor);

				// Draw the scroll text.
				textScroller.Render();

				// Update the physical display.
				SenseHat.Display.Update();

				// Pause for a short while.
				Sleep(TimeSpan.FromMilliseconds(50));
			}
		}
	}
}