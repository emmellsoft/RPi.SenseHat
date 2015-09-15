using System.Linq;

namespace Emmellsoft.IoT.Rpi.SenseHat.Tools.LedBuffer
{
	public static class FindBestGammaMatch
	{
		public static double Best5BitGammaMatch(byte[] wantedGammaTable, double start, double stop, double step)
		{
			var gammaValue = start;

			double bestMatchGammaValue = 0;
			int bestMatchGammaFailCount = int.MaxValue;

			do
			{
				byte[] myGamma = GammaCalc.Get5BitGamma(gammaValue).ToArray();

				int failCount = 0;
				for (int i = 0; i < myGamma.Length; i++)
				{
					if (myGamma[i] != wantedGammaTable[i])
					{
						failCount++;
					}
				}
				
				if (failCount < bestMatchGammaFailCount)
				{
					bestMatchGammaValue = gammaValue;
					bestMatchGammaFailCount = failCount;

					if (failCount == 0)
					{
						break;
					}
				}

				gammaValue += step;
			}
			while (gammaValue < stop);

			return bestMatchGammaValue;
		}
	}
}