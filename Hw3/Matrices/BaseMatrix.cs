namespace Hw3.Matrices
{
	public abstract class BaseMatrix
	{
		public double[,] Similarities { get; }
		public string Name { get; }

		public BaseMatrix(double[,] similarities, string name)
		{
			Similarities = similarities;
			Name = name;
		}
	}
}
