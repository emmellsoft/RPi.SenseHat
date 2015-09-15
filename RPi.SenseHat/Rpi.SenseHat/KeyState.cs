namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public enum KeyState
	{
		/// <summary>
		/// Key is idling.
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

	public static class KeyStateExtensions
	{
		public static bool IsPressed(this KeyState keyState)
		{
			return (keyState == KeyState.Pressed) || (keyState == KeyState.Pressing);
		}
	}
}