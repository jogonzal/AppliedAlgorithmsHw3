using System;

using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public class L2Similarity : SimilarityAlgorithm
	{
		public override double CalculateSimilarity(Article articleX, Article articleY)
		{
			double acc = 0;

			Action<double, double> performStep = (x, y) =>
			{
				double xMinusY = (x - y);
				acc += xMinusY * xMinusY;
			};

			PerformSteps(performStep, articleX, articleY);

			acc = Math.Sqrt(acc);

			return (-1 *  acc);
		}

		public override string Name => "L2";
	}
}
