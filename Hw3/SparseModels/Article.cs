using System.Collections.Generic;

using Hw3.ParsingModels;

namespace Hw3.SparseModels
{
	public class Article
	{
		public int ArticleId { get; }
		private readonly Dictionary<int, int> _wordCounts;

		public Article(int articleId, IList<DataModel> dataModels)
		{
			ArticleId = articleId;
			_wordCounts = new Dictionary<int, int>(dataModels.Count);
			foreach (var dataModel in dataModels)
			{
				_wordCounts.Add(dataModel.WordId, dataModel.Count);
			}
		}

		public int GetWordCount(int wordId)
		{
			int wordCount;
			if (_wordCounts.TryGetValue(wordId, out wordCount))
			{
				return wordCount;
			}

			return 0;
		}
	}
}
