using System;

using Hw3.Matrices;
using Newtonsoft.Json;

namespace Hw3
{
	public static class Problem2
	{
		public static void Run()
		{
			Random rand = new Random();

			// Define k and d as you would like
			int k = 20;
			int d = 20;

			// Create a random matrix with values sampled from a normal distribution
			double[,] matrixWithRandomNormalValues = MatrixCreate.CreateRandomNormalDistributionMatrix(k, d, rand);

			string dump = JsonConvert.SerializeObject(matrixWithRandomNormalValues);

			Console.ReadKey();
		}
	}
}
