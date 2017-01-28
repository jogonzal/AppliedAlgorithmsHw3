using System;
using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public abstract class BaseSimilarity
	{
		public abstract double CalculateSimilarity(Article articleX, Article articleY);

		protected void PerformSteps(Action<double, double> performStep, Article articleX, Article articleY)
		{
			foreach (var wordCountX in articleX.WordCounts)
			{
				int wordCountY;
				if (articleY.WordCounts.TryGetValue(wordCountX.Key, out wordCountY))
				{
					// Both are present - perform the operation
					performStep(wordCountX.Value, wordCountY);
				}
				else
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
		}
	}
}
