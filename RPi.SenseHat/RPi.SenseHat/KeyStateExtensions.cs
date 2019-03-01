namespace Emmellsoft.IoT.RPi.SenseHat
{
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
