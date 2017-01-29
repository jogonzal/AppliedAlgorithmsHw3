using System.Collections.Generic;
using System.Linq;

using Hw3.CsvUtils;
using Hw3.Heatmaps;
using Hw3.Matrices;
using Hw3.NearestNeighbor;
using Hw3.ParsingModels;
using Hw3.Similarities;
using Hw3.SparseModels;

namespace Hw3
{
	public static class Problem1
	{
		public static void Run()
		{
			// First parse content from files
			List<DataModel> dataModels;
			List<GroupModel> groupModels;
			List<LabelModel> labelModels;
			CsvParseUtils.ParseCsvFiles(out dataModels, out groupModels, out labelModels);

			// Then let the grouping begin
			List<Group> groups = Initializer.InitializeAll(dataModels, groupModels, labelModels);

			// Calculate nearest neighbors for every article, using Jacard, in parallel
			SimilarityAlgorithm similarityAlgorithm = new JaccardSimilarity();
			NearestNeighborCalculator.Calculate(groups, similarityAlgorithm);
			ArticleSet articleSet = new ArticleSet(groups, "Normal");

			// Calculate the nearest neighbors matrix with Jacard
			NearestNeighborMatrix nearestNeighborsMatrix = NearestNeighborMatrix.CalculateNearestNeighborMatrix(articleSet, similarityAlgorithm);

			// Calculate similarity matrices for all 3 algorithms in parallel
			List<SimilarityAlgorithm> similarityAlgorithms = new List<SimilarityAlgorithm>()
			{
				new JaccardSimilarity(),
				new CosineSimilarity(),
				new L2Similarity()
			};
			List<SimilarityMatrix> similarityMatrices = similarityAlgorithms.AsParallel().Select(s => SimilarityMatrix.CalculateSimilarityMatrix(articleSet, s)).ToList();

			// Build heatmaps for similarity matrices
			HeatMapBuilder.BuildAndDumpHeatmaps(similarityMatrices);

			// Build heatmaps for #nearestNeighbors
			HeatMapBuilder.BuildAndDumpHeatmaps(new List<BaseMatrix>()
			{
				nearestNeighborsMatrix
			});
		}
	}
}
