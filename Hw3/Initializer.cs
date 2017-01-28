using System.Collections.Generic;

using Hw3.ParsingModels;
using Hw3.SparseModels;

namespace Hw3
{
	public static class Initializer
	{
		public static List<Group> InitializeAll(IList<DataModel> dataModels, IList<GroupModel> groupModels,
			IList<LabelModel> labelModels)
		{
			// First build the groups
			List<Group> groups = new List<Group>(groupModels.Count);
			for (int i = 0; i < groupModels.Count; i++)
			{
				var groupModel = groupModels[i];
				groups.Add(new Group(i, groupModel.GroupName));
			}

			// Now that we have the groups, start building every article, and put it in the right group
			Dictionary<int, List<DataModel>> articleToDataModelMapping = new Dictionary<int, List<DataModel>>();
			foreach (var dataModel in dataModels)
			{
				List<DataModel> currentList = null;
				if (articleToDataModelMapping.TryGetValue(dataModel.ArticleId, out currentList))
				{
					currentList.Add(dataModel);
				}
				else
				{
					articleToDataModelMapping[dataModel.ArticleId] = new List<DataModel>()
					{
						dataModel
					};
				}
			}

			// Now that we have the list of words per article, we can distribute the articles depending on which group
			// they belong to
			foreach(var keyValuePair in articleToDataModelMapping)
			{
				// Build the article
				Article article = new Article(keyValuePair.Key, keyValuePair.Value);
				// Determine the group they belong to
				int groupId = labelModels[article.ArticleId - 1].GroupId;
				groups[groupId - 1].Articles.Add(article);
			}

			return groups;
		}
	}
}
