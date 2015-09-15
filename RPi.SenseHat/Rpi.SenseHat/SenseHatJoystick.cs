using System;

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	internal sealed class SenseHatJoystick : ISenseHatJoystick
	{
		private readonly SenseHat _senseHat;

		public SenseHatJoystick(SenseHat senseHat)
		{
			_senseHat = senseHat;
		}

		public void Update()
		{
			byte state = _senseHat.ReadByte(0xf2);

			bool hasChanged = false;

			LeftKey = GetKeyState(LeftKey, (state & 0x10) > 0, ref hasChanged);
			RightKey = GetKeyState(RightKey, (state & 0x02) > 0, ref hasChanged);
			UpKey = GetKeyState(UpKey, (state & 0x04) > 0, ref hasChanged);
			DownKey = GetKeyState(DownKey, (state & 0x01) > 0, ref hasChanged);
			EnterKey = GetKeyState(EnterKey, (state & 0x08) > 0, ref hasChanged);

			HasChanged = hasChanged;
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

		public bool HasChanged
		{ get; private set; }

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