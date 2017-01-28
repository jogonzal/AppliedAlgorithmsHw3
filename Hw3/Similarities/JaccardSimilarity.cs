using System;

using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public class JaccardSimilarity : BaseSimilarity
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

			foreach (var wordCountX in articleX.WordCounts)
			{
				int wordCountY;
				if (articleY.WordCounts.TryGetValue(wordCountX.Key, out wordCountY))
				{
					// Both are present - perform the operation
					performStep(wordCountX.Value, wordCountY);
				} else
				{
					// Only X is present - perform the operation
					performStep(wordCountX.Value, 0);
				}
			}

			foreach (var wordCountY in articleY.WordCounts)
			{
				if (!articleX.WordCounts.ContainsKey(wordCountY.Key))
				{
					// Only Y is present - perform the operation
					performStep(0, wordCountY.Value);
				}
			}

			return accNumerator / accDenominator;
		}
	}
}
