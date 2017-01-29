using System.Collections.Generic;
using System.IO;
using System.Linq;

using CsvHelper;

using Hw3.ParsingModels;

namespace Hw3.CsvUtils
{
	public static class CsvParseUtils
	{
		public static void ParseCsvFiles(out List<DataModel> dataModels, out List<GroupModel> groupModels, out List<LabelModel> labelModels)
		{
			// Simply go into the CSV files and parse them out - CSVHelper will assign them to the model properties in the order
			// that they show
			string pathToRoot = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\collateral\");
			string pathToData = Path.Combine(pathToRoot, "data50.csv");
			string pathToGroups = Path.Combine(pathToRoot, "groups.csv");
			string pathToLabel = Path.Combine(pathToRoot, "label.csv");

			using (var sr = new StreamReader(pathToData))
			{
				var reader = new CsvReader(sr);
				reader.Configuration.HasHeaderRecord = false;
				dataModels = reader.GetRecords<DataModel>().ToList();
			}

			using (var sr = new StreamReader(pathToGroups))
			{
				var reader = new CsvReader(sr);
				reader.Configuration.HasHeaderRecord = false;
				groupModels = reader.GetRecords<GroupModel>().ToList();
			}

			using (var sr = new StreamReader(pathToLabel))
			{
				var reader = new CsvReader(sr);
				reader.Configuration.HasHeaderRecord = false;
				labelModels = reader.GetRecords<LabelModel>().ToList();
			}
		}
	}
}
