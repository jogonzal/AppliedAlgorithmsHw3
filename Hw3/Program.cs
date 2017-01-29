using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Hw3.CsvUtils;
using Hw3.Heatmaps;
using Hw3.ParsingModels;
using Hw3.Similarities;
using Hw3.SparseModels;
using Newtonsoft.Json;

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

			List<SimilarityAlgorithm> similarityAlgorithms = new List<SimilarityAlgorithm>()
			{
				new JaccardSimilarity(),
				new CosineSimilarity(),
				new L2Similarity()
			};

			List<SimilarityMatrix> result = similarityAlgorithms.AsParallel().Select(similarityAlgorithm => SimilarityMatrix.CalculateSimilarityMatrix(groups, similarityAlgorithm)).ToList();

			HeatMapBuilder.BuildAndDumpHeatmaps(result);
		}
	}
}
