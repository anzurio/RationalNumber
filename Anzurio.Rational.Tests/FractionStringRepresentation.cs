using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anzurio.Rational.Tests
{
    [TestFixture]
    public class FractionStringRepresentation
    {
        [TestCase(-1, 1, 2, "-1_1/2")] // -1_1/2 == -1_1/2
        [TestCase(1, 1, 2, "1_1/2")] // 1_1/2 == 1_1/2
        [TestCase(0, 1, 2, "1/2")] // 0_1/2 == 1/2
        [TestCase(1, 2, 4, "1_1/2")] // 1_2/4 == 1_1/2
        [TestCase(-1, 2, 4, "-1_1/2")] // -1_2/4 == -1_1/2
        [TestCase(1, -2, 4, "-1_1/2")] // -1_2/4 == -1_1/2
        [TestCase(null, 7, 9, "7/9")] // 7/9 == 7/9
        [TestCase(null, 7, -9, "-7/9")] // 7/-9 == -7/9
        [TestCase(null, -1, 2, "-1/2")] // -1/2 == -1/2
        [TestCase(null, 1, 2, "1/2")] // 1/2 == 1/2
        [TestCase(null, 3, 2, "1_1/2")] // 3/2 == 1_1/2
        [TestCase(null, 6, 3, "2")] // 6/3 == 2
        [TestCase(null, -6, 3, "-2")] // -6/3 == -2
        [TestCase(null, 9, 6, "1_1/2")] // 9/6 == 1_1/2
        [TestCase(null, -9, 6, "-1_1/2")] // -6/3 == -1_1/2
        [TestCase(null, 12, 9, "1_1/3")] // 12/9 == 1_1/3
        [TestCase(null, -12, 9, "-1_1/3")] // -12/9 == -1_1/3
        [TestCase(null, 2, 2, "1")]
        [TestCase(null, -2, 2, "-1")]
        [TestCase(1, 2, 2, "2")]
        [TestCase(1, -2, 2, "-2")]
        [TestCase(0, 0, 5, "0")]
        [TestCase(0, 0, -5, "0")]
        [TestCase(null, 0, 5, "0")]
        [TestCase(null, 0, -5, "0")]
        public void ConstructProperFractionStringRepresentation(
            int? whole,
            int numerator,
            int denominator,
            string expectedResult)
        {
            var rationalNumber = new RationalNumber(whole ?? 0, numerator, denominator);
            rationalNumber.ToString().Should().Be(expectedResult);
        }

        [TestCase(-1, 1, 2, "-3/2")] // -1_1/2 == -3/2
        [TestCase(1, 1, 2, "3/2")] // 1_1/2 == 3/2
        [TestCase(0, 1, 2, "1/2")] // 0_1/2 == 1/2
        [TestCase(1, 2, 4, "3/2")] // 1_2/4 == 3/2
        [TestCase(-1, 2, 4, "-3/2")] // -1_2/4 == -3/2
        [TestCase(1, -2, 4, "-3/2")] // -1_2/4 == -3/2
        [TestCase(null, 7, 9, "7/9")] // 7/9 == 7/9
        [TestCase(null, 7, -9, "-7/9")] // 7/-9 == -7/9
        [TestCase(null, -1, 2, "-1/2")] // -1/2 == -1/2
        [TestCase(null, 1, 2, "1/2")] // 1/2 == 1/2
        [TestCase(null, 3, 2, "3/2")] // 3/2 == 3/2
        [TestCase(null, 6, 3, "2")] // 6/3 == 2
        [TestCase(null, -6, 3, "-2")] // -6/3 == -2
        [TestCase(null, 9, 6, "3/2")] // 9/6 == 3/2
        [TestCase(null, -9, 6, "-3/2")] // -6/3 == -3/2
        [TestCase(null, 12, 9, "4/3")] // 12/9 == 4/3
        [TestCase(null, -12, 9, "-4/3")] // -12/9 == -4/3
        [TestCase(null, 2, 2, "1")]
        [TestCase(null, -2, 2, "-1")]
        [TestCase(1, 2, 2, "2")]
        [TestCase(1, -2, 2, "-2")]
        [TestCase(0, 0, 5, "0")]
        [TestCase(0, 0, -5, "0")]
        [TestCase(null, 0, 5, "0")]
        [TestCase(null, 0, -5, "0")]
        public void ConstructImproperFractionStringRepresentation(
            int? whole,
            int numerator,
            int denominator,
            string expectedResult)
        {
            var rationalNumber = new RationalNumber(whole ?? 0, numerator, denominator);
            rationalNumber.ToImproperFractionString().Should().Be(expectedResult);
        }
    }
}
