using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Hw3.CsvUtils;
using Hw3.Heatmaps;
using Hw3.Matrices;
using Hw3.NearestNeighbor;
using Hw3.ParsingModels;
using Hw3.Similarities;
using Hw3.SparseModels;

namespace Hw3
{
	public static class Problem2
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

			// The maximum word id + 1 will be "k"
			int maximumWordId = groups.SelectMany(g => g.Articles).SelectMany(a => a.WordCounts).Max(w => w.Key);
			int k = maximumWordId + 1;

			// Reduce the articles to the dimensions specified below
			List<int> dimensionsToReduceTo = new List<int>()
			{
				100, 50, 25, 10
			};
			ArticleSet originalArticleSet = new ArticleSet(groups, "Normal");
			IEnumerable<DimensionReducedArticleSet> dimensionReducedArticleSets = dimensionsToReduceTo.AsParallel().Select(d => DimensionReducedArticleSet.ReduceToDimensions(k, d, groups)).ToList();
			List<ArticleSet> allArticleSets = new List<ArticleSet>(dimensionReducedArticleSets);
			allArticleSets.Insert(0, originalArticleSet);

			// Now we'll calculate nearest neighbors for all dimension reduced groups
			SimilarityAlgorithm similarityAlgorithm = new CosineSimilarity();

			List<NearestNeighborMatrix> nearestNeighborsMatrices = allArticleSets.AsParallel().Select(a =>
			{
				Stopwatch sw = new Stopwatch();
				sw.Start();
				NearestNeighborCalculator.Calculate(a.Groups, similarityAlgorithm);
				var matrix = NearestNeighborMatrix.CalculateNearestNeighborMatrix(a, similarityAlgorithm);
				sw.Stop();
				matrix.Name += $" ({sw.ElapsedMilliseconds} ms)";
				return matrix;
			}).ToList();

			HeatMapBuilder.BuildAndDumpHeatmaps(nearestNeighborsMatrices);

			List<ScatterPlotInfo> scatterPlotInfos =
				dimensionReducedArticleSets.Select(a => ScatterPlotInfo.GetPlotInfo(originalArticleSet, a, similarityAlgorithm)).ToList();

			ScatterBuilder.BuildAndDumpScatters(scatterPlotInfos);
		}
	}
}
