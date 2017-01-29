using System;

using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public class CosineSimilarity : SimilarityAlgorithm
	{
		public override double CalculateSimilarity(Article articleX, Article articleY)
		{
			double accNumerator = 0;
			double accDenominatorX = 0;
			double accDenominatorY = 0;

			Action<double, double> performStep = (x, y) =>
			{
				accNumerator += x*y;
				accDenominatorX += x * x;
				accDenominatorY += y * y;
			};

			PerformSteps(performStep, articleX, articleY);

			accDenominatorX = Math.Sqrt(accDenominatorX);
			accDenominatorY = Math.Sqrt(accDenominatorY);

			return accNumerator / (accDenominatorX * accDenominatorY);
		}
	}
}
