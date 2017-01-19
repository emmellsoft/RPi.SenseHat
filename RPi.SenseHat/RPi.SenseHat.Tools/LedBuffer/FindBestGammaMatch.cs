////////////////////////////////////////////////////////////////////////////
//
//  This file is part of Rpi.SenseHat.Tools
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