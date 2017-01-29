using System;
using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public abstract class SimilarityAlgorithm
	{
		public abstract double CalculateSimilarity(Article articleX, Article articleY);
		public abstract string Name { get; }

		protected void PerformSteps(Action<double, double> performStep, Article articleX, Article articleY)
		{
			foreach (var wordCountX in articleX.WordCounts)
			{
				double wordCountY;
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
