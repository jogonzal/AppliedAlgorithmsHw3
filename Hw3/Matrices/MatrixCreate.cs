using System;

using Hw3.Distribution;

namespace Hw3.Matrices
{
	public static class MatrixCreate
	{
		public static double[,] CreateRandomNormalDistributionMatrix(int k, int d, Random rand)
		{
			double[,] matrix = new double[d,k];

			for (int r = 0; r < matrix.GetLength(0); r++)
			{
				for (int c = 0; c < matrix.GetLength(1); c++)
				{
					// Mean = 0, stdev = 1
					matrix[r, c] = NormalDistribution.GetRandom(0, 1, rand);
				}
			}

			return matrix;
		}

		public static double[,] CreateRandomPlusOneMinusOneMatrix(int k, int d, Random rand)
		{
			double[,] matrix = new double[d, k];

			for (int r = 0; r < matrix.GetLength(0); r++)
			{
				for (int c = 0; c < matrix.GetLength(1); c++)
				{
					bool randomBoolean = rand.NextDouble() >= 0.5;
					matrix[r, c] = randomBoolean ? 1 : -1;
				}
			}

			return matrix;
		}
	}
}
