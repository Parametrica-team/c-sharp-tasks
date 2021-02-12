using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace PercentageTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AdjustPercenage_Test_99()
        {
            var input = new List<double> {80.4, 9.3, 10.3 };
            var correctResult = new List<int> { 81, 9, 10 };
            var result = Percentage.Program.AdjustPercentage(input);
            Assert.AreEqual(correctResult, result);
        }

        [Test]
        public void AdjustPercenage_Test_RoundUp()
        {
            var input = new List<double> { 80.30, 10.90, 9.30, 5.80, 14.40, 1.30, 1.00 };
            var correctResult = new List<int> { 80, 11, 9, 6, 15, 1, 1 };
            var result = Percentage.Program.AdjustPercentage(input);
            Assert.AreEqual(correctResult, result);
        }

        [Test]
        public void AdjustPercenage_Test_RoundDown()
        {
            var input = new List<double> { 1.9, 1.5, 1.0, 1.6, 1.2, 1.8 };
            var correctResult = new List<int> { 2, 1, 1, 2, 1, 2 };
            var result = Percentage.Program.AdjustPercentage(input);
            Assert.AreEqual(correctResult, result);
        }
    }
}