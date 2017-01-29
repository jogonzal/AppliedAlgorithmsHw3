using System.Collections.Generic;

namespace Hw3.Matrices
{
	public static class SparseMatrixMultiply
	{
		public static Dictionary<int, double> DotProduct(double[,] matrix, IReadOnlyDictionary<int, double> sparseVector)
		{
			Dictionary<int, double> result = new Dictionary<int, double>();
			for (int r = 0; r < matrix.GetLength(0); r++)
			{
				double acc = 0;
				foreach (var element in sparseVector)
				{
					acc += element.Value*matrix[r, element.Key - 1]; // article id indexing
				}

				// Sparse result - only if > 0
				if (acc > 0)
				{
					result[r] = acc;
				}
			}

			return result;
		}
	}
}
