using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hw3.CsvUtils;
using Hw3.ParsingModels;

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


			Console.ReadKey();
		}
	}
}
