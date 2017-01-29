using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Hw3.Similarities;
using Hw3.SparseModels;

namespace Hw3.NearestNeighbor
{
	public static class NearestNeighborCalculator
	{
		public static void Calculate(List<Group> groups, SimilarityAlgorithm similarityAlgorithm)
		{
			// For every article, calculate the nearest neighbor
			// First get the full list of articles
			List<Article> articles = groups.SelectMany(g => g.Articles).ToList();

			// Now for each article, find the nearest neighbor
			Parallel.ForEach(articles, article => FindNearestNeighbor(articles, article, similarityAlgorithm));
		}

		private static void FindNearestNeighbor(List<Article> articles, Article article, SimilarityAlgorithm similarityAlgorithm)
		{
			Article nearestNeighbor = null;
			double maximumSimilarity = 0;
			foreach (var possibleNearestNeighbor in articles)
			{
				if (possibleNearestNeighbor.GroupId == article.GroupId)
				{
					// Skip articles in the same group
					continue;
				}

				double similarity = similarityAlgorithm.CalculateSimilarity(possibleNearestNeighbor, article);
				if (nearestNeighbor == null || similarity > maximumSimilarity)
				{
					maximumSimilarity = similarity;
					nearestNeighbor = possibleNearestNeighbor;
				}
			}
			article.NearestNeighbor = nearestNeighbor;
		}
	}
}
