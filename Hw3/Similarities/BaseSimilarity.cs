using Hw3.SparseModels;

namespace Hw3.Similarities
{
	public abstract class BaseSimilarity
	{
		public abstract double CalculateSimilarity(Article articleX, Article articleY);
	}
}
