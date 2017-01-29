﻿using System.Collections.Generic;
using System.Linq;
using Hw3.Similarities;
using Hw3.SparseModels;

namespace Hw3
{
	public class SimilarityMatrix
	{
		public double[,] Similarities { get; }
		public SimilarityAlgorithm SimilarityAlgorithm { get; }

		private SimilarityMatrix(double[,] similarities, SimilarityAlgorithm similarityAlgorithm)
		{
			Similarities = similarities;
			SimilarityAlgorithm = similarityAlgorithm;
		}

		public static SimilarityMatrix CalculateSimilarityMatrix(List<Group> groups, SimilarityAlgorithm similarityAlgorithm)
		{
			double[,] similarities = new double[groups.Count, groups.Count];

			// For each row column, calculate similarities
			for (int r = 0; r < groups.Count; r++)
			{
				for (int c = r; c < groups.Count; c++)
				{
					double similarity = CalculateSimilarityForGroups(groups[r], groups[c], similarityAlgorithm);
					similarities[r, c] = similarity;
					similarities[c, r] = similarity;
				}
			}

			return new SimilarityMatrix(similarities, similarityAlgorithm);
		}

		private static double CalculateSimilarityForGroups(Group groupA, Group groupB, SimilarityAlgorithm similarityAlgorithm)
		{
			// To calculate the average similarity between articles in group A and group B, we'll have to compare
			// every combination
			List<double> similaritiesBetweenArticles = new List<double>(groupA.Articles.Count * groupB.Articles.Count);

			foreach (var groupAArticle in groupA.Articles)
			{
				foreach (var groupBArticle in groupB.Articles)
				{
					double similarityBetweenArticles = CalculateSimilarityForArticles(groupAArticle, groupBArticle, similarityAlgorithm);
					similaritiesBetweenArticles.Add(similarityBetweenArticles);
				}
			}

			return similaritiesBetweenArticles.Average();
		}

		private static double CalculateSimilarityForArticles(Article groupAArticle, Article groupBArticle, SimilarityAlgorithm similarityAlgorithm)
		{
			return similarityAlgorithm.CalculateSimilarity(groupAArticle, groupBArticle);
		}
	}
}
