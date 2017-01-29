using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;

namespace Hw3.Heatmaps
{
	public static class HeatMapBuilder
	{
		private const string AlgorithmReplacement = "{Algorithm}";
		private const string DataReplacement = "{Data}";

		public static void BuildAndDumpHeatmaps(List<SimilarityMatrix> similarityMatrices)
		{
			// We'll simply use http://jsfiddle.net/chz29mu2/

			// We'll need to make a few string replacements, then we'll be able to build the HTML file from the template
			string pathToHeatMapTemplate = Path.Combine(Directory.GetCurrentDirectory(), @"Heatmaps\HeatMapTemplate.html");
			string heatMapContent = File.ReadAllText(pathToHeatMapTemplate);

			foreach (var similarityMatrix in similarityMatrices)
			{
				string algorithmName = similarityMatrix.SimilarityAlgorithm.Name;
				string data = GetDataForMatrix(similarityMatrix.Similarities);

				string templatizedFile = heatMapContent.Replace(AlgorithmReplacement, algorithmName).Replace(DataReplacement, data);
				string calculatedPath = Path.Combine(Directory.GetCurrentDirectory(), @"GeneratedHeatmap" + algorithmName + ".html");
				File.WriteAllText(calculatedPath, templatizedFile);

				// And open it
				Process.Start(calculatedPath);
			}
		}

		private static string GetDataForMatrix(double[,] similarityMatrixSimilarities)
		{
			// Example data  = [[0,0,-0.1], [1,0,-2.1], [2,0,-1.1],[0,1,-0.7], [1,1,-3.4], [2,1,-1.1]]
			double[,] arrToSerialize = new double[similarityMatrixSimilarities.GetLength(0) * similarityMatrixSimilarities.GetLength(1), 3];

			int currentCoord = 0;
			for (int r = 0; r < similarityMatrixSimilarities.GetLength(0); r++)
			{
				for (int c = 0; c < similarityMatrixSimilarities.GetLength(1); c++)
				{
					double similarity = similarityMatrixSimilarities[r, c];
					arrToSerialize[currentCoord, 0] = r; 
					arrToSerialize[currentCoord, 1] = c; 
					arrToSerialize[currentCoord, 2] = similarity;
					currentCoord++;
				}
			}

			return JsonConvert.SerializeObject(arrToSerialize);
		}
	}
}
