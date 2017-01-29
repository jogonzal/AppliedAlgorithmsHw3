using System;

namespace Hw3.Distribution
{
	public static class NormalDistribution
	{
		public static double GetRandom(int mean, int stdDev, Random rand)
		{
			// Box-Muller transform to sample numbers from a normal distribution
			double u1 = rand.NextDouble(); //these are uniform(0,1) random doubles
			double u2 = rand.NextDouble();
			double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
						 Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
			double randNormal =
						 mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

			return randNormal;
		}
	}
}
