using Emmellsoft.IoT.Rpi.SenseHat;

namespace RPi.SenseHat.Demo
{
	public abstract class SenseHatDemo
	{
		protected ISenseHat SenseHat
		{ get; private set; }

		protected SenseHatDemo(ISenseHat senseHat)
		{
			SenseHat = senseHat;
		}

		public abstract void Update();
	}
}