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
            var result = Percentage.Program.AdjustPercentage(input);
            Assert.AreEqual(100, result.Sum());
        }

        [Test]
        public void AdjustPercenage_Test_RoundMax()
        {
            var input = new List<double> { 9.3, 80.4, 10.3 };
            var result = Percentage.Program.AdjustPercentage(input);
            Assert.AreEqual(81, result[1], "80.4 должно было округлиться до 81");
        }

        

        [Test]
        public void AdjustPercenage_Test_RoundMax2()
        {
            var input = new List<double> { 80.30, 10.90, 9.30, 5.80, 14.40, 1.30, 1.00 };
            var result = Percentage.Program.AdjustPercentage(input);
            Assert.AreEqual(15, result[4], "14.4 должно было округлиться до 15");
        }
    }
}