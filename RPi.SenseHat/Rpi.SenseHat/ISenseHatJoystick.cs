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
	/// Interface to the Sense HAT joystick.
	/// </summary>
	public interface ISenseHatJoystick
	{
		/// <summary>
		/// Reads the current status of the joystick and updates the properties of this interface.
		/// Returns true if it was changed since the last time.
		/// </summary>
		bool Update();

		/// <summary>
		/// The state of the LEFT joystick key.
		/// </summary>
		KeyState LeftKey
		{ get; }

		/// <summary>
		/// The state of the RIGHT joystick key.
		/// </summary>
		KeyState RightKey
		{ get; }

		/// <summary>
		/// The state of the UP joystick key.
		/// </summary>
		KeyState UpKey
		{ get; }

		/// <summary>
		/// The state of the DOWN joystick key.
		/// </summary>
		KeyState DownKey
		{ get; }

		/// <summary>
		/// The state of the MIDDLE joystick key.
		/// </summary>
		KeyState EnterKey
		{ get; }
	}
}