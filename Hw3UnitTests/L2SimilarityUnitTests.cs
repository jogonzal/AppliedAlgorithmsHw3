using System.Collections.Generic;

using FluentAssertions;

using Hw3.Similarities;
using Hw3.SparseModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hw3UnitTests
{
	[TestClass]
	public class L2SimilarityUnitTests
	{
		[TestMethod]
		public void TestWithVector_ShouldBe0()
		{
			var jac = new L2Similarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{2, 2},
				{3, 3}
			}));

			result.Should().BeInRange(-4.25, -4.24); // -4.242640687
		}

		[TestMethod]
		public void TestWithVector_ShouldBe1()
		{
			var jac = new L2Similarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}));

			result.Should().Be(0);
		}

		[TestMethod]
		public void TestWithVector_ShouldBe05()
		{
			var jac = new L2Similarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, double>()
			{
				{0, 1},
				{1, 5}
			}));

			result.Should().Be(-3);
		}

		[TestMethod]
		public void TestWithVector_ShouldBeDecimal()
		{
			var jac = new L2Similarity();
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

			result.Should().BeInRange(-10.45, -10.44); // - 10.44030651
		}

		[TestMethod]
		public void TestWithVector_ShouldBeDecimal2()
		{
			var jac = new L2Similarity();
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

			result.Should().BeInRange(-14.46, -14.45); // - 14.45683229
		}
	}
}
