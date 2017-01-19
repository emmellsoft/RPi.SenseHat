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

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	/// <summary>
	/// The state of a joystick key.
	/// </summary>
	public enum KeyState
	{
		/// <summary>
		/// The key is idling.
		/// </summary>
		Released,

		/// <summary>
		/// The key is just pressed.
		/// </summary>
		Pressing,

		/// <summary>
		/// The key is held pressed.
		/// </summary>
		Pressed,

		/// <summary>
		/// The key is just released.
		/// </summary>
		Releasing,
	}

	/// <summary>
	/// Extension methods to the KeyState enum.
	/// </summary>
	public static class KeyStateExtensions
	{
		/// <summary>
		/// Is the key currently pressed?
		/// </summary>
		/// <param name="keyState">The KeyState to check.</param>
		/// <returns></returns>
		public static bool IsPressed(this KeyState keyState)
		{
			return (keyState == KeyState.Pressed) || (keyState == KeyState.Pressing);
		}
	}
}