using System.Collections.Generic;
using System.IO;
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
	class Program
	{
		static void Main(string[] args)
		{
			// First parse content from files
			List<DataModel> dataModels;
			List<GroupModel> groupModels;
			List<LabelModel> labelModels;
			CsvParseUtils.ParseCsvFiles(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\collateral\"), out dataModels, out groupModels, out labelModels);

			// Then let the grouping and hashing begin
			List<Group> groups = Initializer.InitializeAll(dataModels, groupModels, labelModels);

			// Calculate nearest neighbors for every article, using Jacard, in parallel
			NearestNeighborCalculator.Calculate(groups, new JaccardSimilarity());

			// Calculate the nearest neighbors matrix with Jacard
			NearestNeighborMatrix nearestNeighborsMatrix = NearestNeighborMatrix.CalculateNearestNeighborMatrix(groups);

			// Calculate similarity matrices for all 3 algorithms in parallel
			List<SimilarityAlgorithm> similarityAlgorithms = new List<SimilarityAlgorithm>()
			{
				new JaccardSimilarity(),
				new CosineSimilarity(),
				new L2Similarity()
			};
			List<SimilarityMatrix> similarityMatrices = similarityAlgorithms.AsParallel().Select(similarityAlgorithm => SimilarityMatrix.CalculateSimilarityMatrix(groups, similarityAlgorithm)).ToList();

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
