using NUnit.Framework;
using RomanMath.Impl;

namespace RomanMath.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void DivisionReturnsZero()
		{
			var result = Service.Evaluate("IV+II/V");
			Assert.AreEqual(0, result);
		}

		[Test]
		public void AdditionTwoPlusSevenReturnsNine()
		{
			var result = Service.Evaluate("II+VII");
			Assert.AreEqual(9, result);
		}

		[Test]
		public void AllAvailableOptions()
		{
			var result = Service.Evaluate("M+D*C-L+X*V-I");
			Assert.AreEqual(50999, result);
		}
	}
}