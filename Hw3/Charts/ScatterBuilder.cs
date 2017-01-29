using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Hw3.Matrices;

using Newtonsoft.Json;

namespace Hw3.Heatmaps
{
	public static class ScatterBuilder
	{
		private const string ChartTitleReplacement = "{ChartTitle}";
		private const string DimensionsReplacement = "{Dimensions}";
		private const string DataReplacement = "{Data}";

		public static void BuildAndDumpScatters(IEnumerable<ScatterPlotInfo> scatterPlotInfos)
		{
			// We'll simply use http://jsfiddle.net/3r4s8455/

			// We'll need to make a few string replacements, then we'll be able to build the HTML file from the template
			string pathToHeatMapTemplate = Path.Combine(Directory.GetCurrentDirectory(), @"Charts\ScatterTemplate.html");
			string heatMapContent = File.ReadAllText(pathToHeatMapTemplate);

			foreach (var scatterPlotInfo in scatterPlotInfos)
			{
				string data = JsonConvert.SerializeObject(scatterPlotInfo.Coordinates);
				string chartTitle = scatterPlotInfo.Name;
				int dimensions = scatterPlotInfo.Dimensions;

				string templatizedFile = heatMapContent.Replace(ChartTitleReplacement, chartTitle).Replace(DataReplacement, data).Replace(DimensionsReplacement, dimensions.ToString());
				string calculatedPath = Path.Combine(Directory.GetCurrentDirectory(), @"GeneratedScatter" + chartTitle + ".html");
				File.WriteAllText(calculatedPath, templatizedFile);

				// And open it
				Process.Start(calculatedPath);
			}
		}
	}
}
