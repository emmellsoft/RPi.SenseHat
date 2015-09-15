namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public interface ISenseHatJoystick
	{
		void Update();

		bool HasChanged
		{ get; }

		KeyState LeftKey
		{ get; }

		KeyState RightKey
		{ get; }

		KeyState UpKey
		{ get; }

		KeyState DownKey
		{ get; }

		KeyState EnterKey
		{ get; }
	}
}