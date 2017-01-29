using System;
using System.Collections.Generic;
using FluentAssertions;
using Hw3.Similarities;
using Hw3.SparseModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hw3UnitTests
{
	[TestClass]
	public class JaccardSimilarityUnitTests
	{
		[TestMethod]
		public void TestWithVector_ShouldBe0()
		{
			var jac = new JaccardSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, int>()
			{
				{2, 2},
				{3, 3}
			}));

			result.Should().Be(0);
		}

		[TestMethod]
		public void TestWithVector_ShouldBe1()
		{
			var jac = new JaccardSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 2}
			}));

			result.Should().Be(1);
		}

		[TestMethod]
		public void TestWithVector_ShouldBe05()
		{
			var jac = new JaccardSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 5}
			}));

			result.Should().Be(1.0 * 3 / 6);
		}

		[TestMethod]
		public void TestWithVector_ShouldBeDecimal()
		{
			var jac = new JaccardSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 2}
			}), new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 5},
				{2, 10}
			}));

			result.Should().Be(1.0 * 3 / 16);
		}

		[TestMethod]
		public void TestWithVector_ShouldBeDecimal2()
		{
			var jac = new JaccardSimilarity();
			double result = jac.CalculateSimilarity(new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 2},
				{3, 10},
			}), new Article(new Dictionary<int, int>()
			{
				{0, 1},
				{1, 5},
				{2, 10}
			}));

			result.Should().Be(1.0 * 3 / 26);
		}
	}
}
