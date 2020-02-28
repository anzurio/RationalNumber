using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anzurio.Rational.Tests
{
    [TestFixture]
    public class RationalNumberConstruction
    {
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        public void ConstructRationalNumberFromWholeNumber(int wholeNumber)
        {
            var rationalNumber = new RationalNumber(wholeNumber);
            rationalNumber.Denominator.Should().Be(1);
            rationalNumber.Numerator.Should().Be(wholeNumber);
        }

        [TestCase(-1, 1, 2, -3, 2)] // -1_1/2 == -3/2
        [TestCase(1, 1, 2, 3, 2)] // 1_1/2 == 3/2
        [TestCase(0, 1, 2, 1, 2)] // 0_1/2 == 1/2
        [TestCase(1, 2, 4, 3, 2)] // 1_2/4 == 3/2
        [TestCase(-1, 2, 4, -3, 2)] // -1_2/4 == -3/2
        [TestCase(2, 2, 2, 3, 1)]
        [TestCase(-2, 2, 2, -3, 1)]
        public void ConstructRationalNumberProvidingWholeNumberPlusFraction(
            int wholeNumber, 
            int numerator, 
            int denominator,
            int expectedNumerator,
            int expectedDenominator)
        {
            var rationalNumber = new RationalNumber(wholeNumber, numerator, denominator);
            rationalNumber.Denominator.Should().Be(expectedDenominator);
            rationalNumber.Numerator.Should().Be(expectedNumerator);
        }

        [TestCase(-1, 2, -1, 2)] // -1/2 == -1/2
        [TestCase(1, 2, 1, 2)] // 1/2 == 1/2
        [TestCase(3, 2, 3, 2)] // 3/2 == 3/2
        [TestCase(6, 3, 2, 1)] // 6/3 == 2/1
        [TestCase(-6, 3, -2, 1)] // -6/3 == -2/1
        [TestCase(9, 6, 3, 2)] // 9/6 == 3/2
        [TestCase(-9, 6, -3, 2)] // -6/3 == -3/2
        [TestCase(12, 9, 4, 3)] // 12/9 == 4/3
        [TestCase(-12, 9, -4, 3)] // -12/9 == -4/3
        [TestCase(2, 2, 1, 1)]
        [TestCase(-2, 2, -1, 1)]
        public void ConstructRationalNumber(
            int numerator,
            int denominator,
            int expectedNumerator,
            int expectedDenominator)
        {
            var rationalNumber = new RationalNumber(numerator, denominator);
            rationalNumber.Denominator.Should().Be(expectedDenominator);
            rationalNumber.Numerator.Should().Be(expectedNumerator);
        }

        [TestCase]
        public void ConstructNotANumberRationalNumber()
        {
            Action createNotANumberRational = () => new RationalNumber(1, 0);
            Action createNotANumberRationalWithWholeNumber = () => new RationalNumber(0, 1, 0);

            createNotANumberRational.Should().Throw<NotARationalNumberException>();
            createNotANumberRationalWithWholeNumber.Should().Throw<NotARationalNumberException>();
        }

        [TestCase(-1, -1, null)]
        [TestCase(1, -1, -1)]
        [TestCase(-1, 1, -1)]
        [TestCase(-1, -1, 1)]
        [TestCase(-1, -1, -1)]
        public void ConstructInvalidRationalNumberByRepeatedMinusSign(
            int numerator,
            int denominator,
            int? wholeNumber)
        {
            Action createNotANumberRational = () => 
            {
                if (wholeNumber == null)
                {
                    new RationalNumber(numerator, denominator);
                }
                else
                {
                    new RationalNumber(wholeNumber ?? 0, numerator, denominator);
                }
            };

            createNotANumberRational.Should().Throw<InvalidRationalNumber>();
        }
    }
}
