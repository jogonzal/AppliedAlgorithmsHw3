using System;
using System.Collections.Generic;

using Hw3.Matrices;
using Hw3.SparseModels;

namespace Hw3
{
	public class DimensionReducedArticleSet : ArticleSet
	{
		public int D { get; }

		public DimensionReducedArticleSet(List<Group> groups, int d) : base(groups, "Reduced dimension to " + d)
		{
			D = d;
		}

		public static DimensionReducedArticleSet ReduceToDimensions(int k, int d, List<Group> groups)
		{
			Random rand = new Random();

			// Create a random matrix with values sampled from a normal distribution
			// NOTE - uncomment this code to use a gaussian/plusoneminusone matrix
			double[,] matrixForDimensionReduction = MatrixCreate.CreateRandomNormalDistributionMatrix(k, d, rand);
			//double[,] matrixForDimensionReduction = MatrixCreate.CreateRandomPlusOneMinusOneMatrix(k, d, rand);

			List<Group> newGroups = new List<Group>(groups.Count);
			// We'll have to create a random matrix of k x d, where k is the maximum number of words we can have
			foreach (var group in groups)
			{
				Group newGroup = new Group(group.GroupId, group.GroupName);
				List<Article> newArticles = new List<Article>(group.Articles.Count);

				foreach (var article in group.Articles)
				{
					Article newArticle = ReduceArticleDimension(article, matrixForDimensionReduction);
					newArticles.Add(newArticle);
				}

				newGroups.Add(newGroup);
				newGroup.Articles = newArticles;
			}
			return new DimensionReducedArticleSet(newGroups, d);
		}

		private static Article ReduceArticleDimension(Article article, double[,] matrixForDimensionReduction)
		{
			// To redice the dimension of the article, we'll have to compute the dot product
			var newWordCounts = SparseMatrixVectorDotProduct.DotProduct(matrixForDimensionReduction, article.WordCounts);

			return new Article(newWordCounts, article.ArticleId, article.GroupId);
		}
	}
}
