////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat
//
//  Copyright (c) 2017, Mattias Larsson
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

#if NETFX_CORE
using Windows.UI;
#else
using System.Drawing;
#endif

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	/// <summary>
	/// Interface to the Sense HAT display.
	/// </summary>
	public interface ISenseHatDisplay
	{
		/// <summary>
		/// The virtual screen array.
		/// Note: Changes to this property requires a call to Update() to affect the physical screen.
		/// </summary>
		Color[,] Screen
		{ get; }

		/// <summary>
		/// The direction of the screen.
		/// </summary>
		DisplayDirection Direction
		{ get; set; }

		/// <summary>
		/// Set to true to mirror the display horizontally.
		/// </summary>
		bool FlipHorizontal
		{ get; set; }

		/// <summary>
		/// Set to true to mirror the display vertically.
		/// </summary>
		bool FlipVertical
		{ get; set; }

		/// <summary>
		/// The gamma of the red color component. 
		/// </summary>
		double RedGamma
		{ get; set; }

		/// <summary>
		/// The gamma of the green color component. 
		/// </summary>
		double GreenGamma
		{ get; set; }

		/// <summary>
		/// The gamma of the blue color component. 
		/// </summary>
		double BlueGamma
		{ get; set; }

		/// <summary>
		/// Resets the screen with the start-up rainbow colors.
		/// Note: Requires a call to Update() to affect the physical screen.
		/// </summary>
		void Reset();

		/// <summary>
		/// Fills the screen with Black color.
		/// Note: Requires a call to Update() to affect the physical screen.
		/// </summary>
		void Clear();

		/// <summary>
		/// Fills the screen of the given color.
		/// Note: Requires a call to Update() to affect the physical screen.
		/// </summary>
		void Fill(Color color);

		/// <summary>
		/// Copies the colors in the Screen property array to the physical Sense HAT display.
		/// </summary>
		void Update();

		/// <summary>
		/// Copies the given array of colors to the Screen property array.
		/// Note: Requires a call to Update() to affect the physical screen.
		/// </summary>
		/// <param name="colors">2D array of colors. Must have the dimensions 8 x 8.</param>
		/// <param name="offsetX">The screen horizontal offset.</param>
		/// <param name="offsetY">The screen vertical offset.</param>
		void CopyColorsToScreen(Color[,] colors, int offsetX = 0, int offsetY = 0);

		/// <summary>
		/// Copies the given array of colors to the Screen property array.
		/// Note: Requires a call to Update() to affect the physical screen.
		/// </summary>
		/// <param name="offsetX">The screen horizontal offset.</param>
		/// <param name="offsetY">The screen vertical offset.</param>
		/// <param name="colors">1D array of colors. Must be 64 colors long (i.e. 8 rows of 8 pixels each after each other).</param>
		void CopyColorsToScreen(Color[] colors, int offsetX = 0, int offsetY = 0);

		/// <summary>
		/// Copies the current Screen property array to the given array of colors.
		/// </summary>
		/// <param name="colors">2D array of colors. Must have the dimensions 8 x 8.</param>
		void CopyScreenToColors(Color[,] colors);

		/// <summary>
		/// Copies the given array of colors to the Screen property array.
		/// Note: Requires a call to Update() to affect the physical screen.
		/// </summary>
		/// <param name="colors">1D array of colors. Must be 64 colors long (i.e. 8 rows of 8 pixels each after each other).</param>
		void CopyScreenToColors(Color[] colors);

		/// <summary>
		/// Read the raw data of the physical display. Will return 192 bytes.
		/// 
		/// The layout of the LED buffer:
		/// Row 1: R R R R R R R R G G G G G G G G B B B B B B B B
		/// Row 2: R R R R R R R R G G G G G G G G B B B B B B B B
		/// ...
		/// Row 8: R R R R R R R R G G G G G G G G B B B B B B B B
		/// 
		/// Note: Each color component is only 5 bits long, i.e. 0=darkest and 31=brightest.
		/// </summary>
		/// <returns></returns>
		byte[] ReadRaw();

		/// <summary>
		/// Write raw data to the physical display.
		/// The raw buffer must be 192 bytes long.
		/// 
		/// The layout of the LED buffer:
		/// Row 1: R R R R R R R R G G G G G G G G B B B B B B B B
		/// Row 2: R R R R R R R R G G G G G G G G B B B B B B B B
		/// ...
		/// Row 8: R R R R R R R R G G G G G G G G B B B B B B B B
		/// 
		/// Note: Each color component is only 5 bits long, i.e. 0=darkest and 31=brightest.
		/// </summary>
		/// <param name="rawBuffer">192 bytes</param>
		void WriteRaw(byte[] rawBuffer);
	}
}