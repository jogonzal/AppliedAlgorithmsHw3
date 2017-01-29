using System.Collections.Generic;

using Hw3.ParsingModels;

namespace Hw3.SparseModels
{
	public class Article
	{
		public int ArticleId { get; }
		public int GroupId { get; }
		public IReadOnlyDictionary<int, int> WordCounts { get; }

		public Article(int articleId, int groupId, IList<DataModel> dataModels)
		{
			ArticleId = articleId;
			GroupId = groupId;
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

		public Article NearestNeighbor { get; set; }
	}
}
