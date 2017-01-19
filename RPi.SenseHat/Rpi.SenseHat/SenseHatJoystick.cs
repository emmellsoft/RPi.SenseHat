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

using System;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHatJoystick : ISenseHatJoystick
	{
		private readonly MainI2CDevice _mainI2CDevice;
		private bool _isFirstUpdate = true;

		public SenseHatJoystick(MainI2CDevice mainI2CDevice)
		{
			_mainI2CDevice = mainI2CDevice;
		}

		public bool Update()
		{
			byte state = _mainI2CDevice.ReadByte(0xf2);

			bool hasChanged = false;

			LeftKey = GetKeyState(LeftKey, (state & 0x10) > 0, ref hasChanged);
			RightKey = GetKeyState(RightKey, (state & 0x02) > 0, ref hasChanged);
			UpKey = GetKeyState(UpKey, (state & 0x04) > 0, ref hasChanged);
			DownKey = GetKeyState(DownKey, (state & 0x01) > 0, ref hasChanged);
			EnterKey = GetKeyState(EnterKey, (state & 0x08) > 0, ref hasChanged);

			if (_isFirstUpdate)
			{
				_isFirstUpdate = false;
				return true;
			}

			return hasChanged;
		}

		private static KeyState GetKeyState(KeyState lastState, bool isKeyPressed, ref bool hasChanged)
		{
			switch (lastState)
			{
				case KeyState.Released:
					if (isKeyPressed)
					{
						hasChanged = true;
					}

					return isKeyPressed ? KeyState.Pressing : KeyState.Released;

				case KeyState.Pressing:
					hasChanged = true;
					return isKeyPressed ? KeyState.Pressed : KeyState.Releasing;

				case KeyState.Pressed:
					if (!isKeyPressed)
					{
						hasChanged = true;
					}

					return isKeyPressed ? KeyState.Pressed : KeyState.Releasing;

				case KeyState.Releasing:
					hasChanged = true;
					return isKeyPressed ? KeyState.Pressing : KeyState.Released;

				default:
					throw new ArgumentOutOfRangeException(nameof(lastState), lastState, null);
			}
		}

		public KeyState LeftKey
		{ get; private set; }

		public KeyState RightKey
		{ get; private set; }

		public KeyState UpKey
		{ get; private set; }

		public KeyState DownKey
		{ get; private set; }

		public KeyState EnterKey
		{ get; private set; }
	}
}