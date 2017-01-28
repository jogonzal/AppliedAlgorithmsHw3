using System;
using System.Collections.Generic;
using System.IO;

using Hw3.CsvUtils;
using Hw3.ParsingModels;
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

			Console.ReadKey();
		}
	}
}
