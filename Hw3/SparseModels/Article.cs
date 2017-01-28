using System.Collections.Generic;

using Hw3.ParsingModels;

namespace Hw3.SparseModels
{
	public class Article
	{
		public int ArticleId { get; }
		public IReadOnlyDictionary<int, int> WordCounts { get; }

		public Article(int articleId, IList<DataModel> dataModels)
		{
			ArticleId = articleId;
			var wordCounts = new Dictionary<int, int>(dataModels.Count);
			foreach (var dataModel in dataModels)
			{
				wordCounts.Add(dataModel.WordId, dataModel.Count);
			}
			WordCounts = wordCounts;
		}

		public Article(IReadOnlyDictionary<int, int> wordCounts)
		{
			WordCounts = wordCounts;
		}
	}
}
