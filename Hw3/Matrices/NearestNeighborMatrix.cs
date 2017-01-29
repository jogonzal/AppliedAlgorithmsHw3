using System.Collections.Generic;

using Hw3.Matrices;
using Hw3.SparseModels;

namespace Hw3
{
	public class NearestNeighborMatrix : BaseMatrix
	{
		public override double[,] Similarities { get; }

		public override string Name => "Nearest neighbors using Jacard";

		private NearestNeighborMatrix(double[,] similarities)
		{
			Similarities = similarities;
		}

		public static NearestNeighborMatrix CalculateNearestNeighborMatrix(List<Group> groups)
		{
			double[,] similarities = new double[groups.Count, groups.Count];

			// For each row column, calculate similarities
			for (int r = 0; r < groups.Count; r++)
			{
				for (int c = 0; c < groups.Count; c++)
				{
					double similarity = CalculateNearestNeighborsForGroups(groups[r], groups[c]);
					similarities[r, c] = similarity;
				}
			}

			return new NearestNeighborMatrix(similarities);
		}

		private static double CalculateNearestNeighborsForGroups(Group groupA, Group groupB)
		{
			// We'll find out how many articles in group A have nearest neghbors in groupB
			double totalNearestNeighbors = 0;
			foreach (var groupAArticle in groupA.Articles)
			{
				if (groupAArticle.NearestNeighbor.GroupId == groupB.GroupId)
				{
					totalNearestNeighbors++;
				}
			}
			return totalNearestNeighbors;
		}
	}
}
