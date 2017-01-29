using System.Collections.Generic;

using Hw3.SparseModels;

namespace Hw3
{
	public class ArticleSet
	{
		public ArticleSet(List<Group> groups, string name)
		{
			Groups = groups;
			Name = name;
		}

		public List<Group> Groups { get; }
		public string Name { get; set; }
	}
}
