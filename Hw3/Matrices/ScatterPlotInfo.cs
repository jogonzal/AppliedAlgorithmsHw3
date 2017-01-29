using System.Collections.Generic;
using System.Linq;
using Hw3.Similarities;

namespace Hw3.Matrices
{
	public class ScatterPlotInfo
	{
		public List<double[]> Coordinates { get; set; }
		public string Name { get; set; }
		public int Dimensions { get; set; }

		public ScatterPlotInfo(string name, int dimensions, List<double[]> coordinates)
		{
			Name = name;
			Dimensions = dimensions;
			Coordinates = coordinates;
		}

		public static ScatterPlotInfo GetPlotInfo(ArticleSet originalArticleSet, DimensionReducedArticleSet articleSet, SimilarityAlgorithm similarityAlgorithm)
		{
			var totalArticles = originalArticleSet.Groups.SelectMany(g => g.Articles).Count();
			List<double[]> coordinates = new List<double[]>(totalArticles);

			var originalArticle3 = originalArticleSet.Groups.SelectMany(g => g.Articles).Single(a => a.ArticleId == 3);
			var article3 = articleSet.Groups.SelectMany(g => g.Articles).Single(a => a.ArticleId == 3);
			for (int g = 0; g < originalArticleSet.Groups.Count; g++)
			{
				var originalGroup = originalArticleSet.Groups[g];
				var group = articleSet.Groups[g];
				for (int a = 0; a < originalGroup.Articles.Count; a++)
				{
					var originalArticle = originalGroup.Articles[a];
					var article = group.Articles[a];

					var originalSimilarityTo3 = similarityAlgorithm.CalculateSimilarity(originalArticle, originalArticle3);
					var similarityTo3 = similarityAlgorithm.CalculateSimilarity(article, article3);

					coordinates.Add(new double[]
					{
						originalSimilarityTo3,
						similarityTo3
					});
				}
			}

			return new ScatterPlotInfo($"Scatter plot for article 3 similarities ({articleSet.D} dimensions)", articleSet.D, coordinates);
        }
	}
}
