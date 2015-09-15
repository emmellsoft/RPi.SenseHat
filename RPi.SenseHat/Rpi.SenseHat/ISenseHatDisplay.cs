#if NETFX_CORE
using Windows.UI;
#else
using System.Drawing;
#endif

namespace Emmellsoft.IoT.Rpi.SenseHat
{
	public interface ISenseHatDisplay
	{
		Color[,] Screen
		{ get; }

		DisplayDirection Direction
		{ get; set; }

		bool FlipHorizontal
		{ get; set; }

		bool FlipVertical
		{ get; set; }

		void Reset();

		void Clear();

		void Fill(Color color);

		void Update();

		void CopyColorsToScreen(Color[,] colors);

		void CopyColorsToScreen(Color[] colors);

		void CopyScreenToColors(Color[,] colors);

		void CopyScreenToColors(Color[] colors);
	}
}