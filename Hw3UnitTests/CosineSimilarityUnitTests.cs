using System.Collections.Generic;

using FluentAssertions;

using Hw3.Similarities;
using Hw3.SparseModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hw3UnitTests
{
	[TestClass]
	public class CosineSimilarityUnitTests
	{
		[TestMethod]
		public void TestWithVector_ShouldBe0()
		{
			var jac = new CosineSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{2, 2},
				{3, 3}
			}));

			result.Should().Be(0);
		}

		[TestMethod]
		public void TestWithVector_ShouldBe1()
		{
			var jac = new CosineSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}));

			result.Should().BeInRange(0.99, 1.01); // 1.0
		}

		[TestMethod]
		public void TestWithVector_ShouldBe05()
		{
			var jac = new CosineSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 5}
			}));

			result.Should().BeInRange(0.963, 0.965); // 0.964763821
		}

		[TestMethod]
		public void TestWithVector_ShouldBeDecimal()
		{
			var jac = new CosineSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 5},
				{2, 10}
			}));

			result.Should().BeInRange(0.438, 0.439); // 0.4385
		}

		[TestMethod]
		public void TestWithVector_ShouldBeDecimal2()
		{
			var jac = new CosineSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2},
				{3, 10},
			}), new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 5},
				{2, 10}
			}));

			result.Should().BeInRange(0.0956, 0.0957); // 0.095634097
		}
	}
}
