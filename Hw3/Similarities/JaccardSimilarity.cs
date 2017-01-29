using System;

using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public class JaccardSimilarity : SimilarityAlgorithm
	{
		public override double CalculateSimilarity(Article articleX, Article articleY)
		{
			double accNumerator = 0;
			double accDenominator = 0;

			Action<double, double> performStep = (x, y) =>
			{
				accNumerator += Math.Min(x, y);
				accDenominator += Math.Max(x, y);
			};

			PerformSteps(performStep, articleX, articleY);

			return accNumerator / accDenominator;
		}
	}
}
