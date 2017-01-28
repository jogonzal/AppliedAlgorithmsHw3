using System.Collections.Generic;

namespace Hw3.SparseModels
{
	public class Group
	{
		public int GroupId { get; set; }
		public string GroupName { get; set; }

		public List<Article> Articles { get; set; }

		public Group(int groupId, string groupName)
		{
			GroupId = groupId;
			GroupName = groupName;
			Articles = new List<Article>();
		}
	}
}
