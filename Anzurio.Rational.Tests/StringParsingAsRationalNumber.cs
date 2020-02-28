using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anzurio.Rational.Tests
{
    [TestFixture]
    public class StringParsingAsRationalNumber
    {
        [TestCase("-3/2", "-3/2")]
        [TestCase("1_1/2", "3/2")]
        [TestCase("0_1/2", "1/2")]
        [TestCase("1_2/4", "3/2")]
        [TestCase("-1_2/4", "-3/2")]
        [TestCase("7/9", "7/9")]
        [TestCase("-1/2", "-1/2")]
        [TestCase("1/2", "1/2")]
        [TestCase("3/2", "3/2")]
        [TestCase("6/3", "2")]
        [TestCase("-6/3", "-2")]
        [TestCase("9/6", "3/2")]
        [TestCase("-9/6", "-3/2")]
        [TestCase("12/9", "4/3")]
        [TestCase("-12/9", "-4/3")]
        [TestCase("2/2", "1")]
        [TestCase("-2/2", "-1")]
        [TestCase("1_2/2", "2")]
        [TestCase("0_0/5", "0")]
        [TestCase("0/5", "0")]
        [TestCase("-5", "-5")]
        [TestCase("2", "2")]
        [TestCase("-0", "0")]
        public void ConstructFractionFromStringParsing(string rationalNumber, string expectedResult)
        {
            var isValidFractionString = RationalNumber.TryParse(rationalNumber, out RationalNumber result);
            isValidFractionString.Should().BeTrue();
            result.ToImproperFractionString().Should().Be(expectedResult);
        }

        [TestCase("7/-9")]
        [TestCase("1_-2/2")]
        [TestCase("0_0/-5")]
        [TestCase("0/-5")]
        [TestCase("1_-2/4")]
        [TestCase("")]
        [TestCase(null)]
        [TestCase("A0")]
        [TestCase("7 _7/9")]
        [TestCase("9_8/")]
        [TestCase("9_8")]
        [TestCase("_9/8")]
        [TestCase("-1_2/0")]
        [TestCase("1_1/0")]
        [TestCase("5/0")]
        [TestCase("-1/0")]
        public void AttemptToParseInvalidFractionFromStringParsing(string rationalNumber)
        {
            var isValidFractionString = RationalNumber.TryParse(rationalNumber, out RationalNumber result);
            isValidFractionString.Should().BeFalse();
        }

        [TestCase]
        public void ParseANullStringAsARationalNumber()
        {
            Action action = () => RationalNumber.Parse(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestCase("7/-9")]
        [TestCase("1_-2/2")]
        [TestCase("0_0/-5")]
        [TestCase("0/-5")]
        [TestCase("1_-2/4")]
        [TestCase("")]
        [TestCase("A0")]
        [TestCase("7 _7/9")]
        [TestCase("9_8/")]
        [TestCase("9_8")]
        [TestCase("_9/8")]
        public void ParseAnInvalidFormatStringAsARationalNumber(string rationalNumber)
        {
            Action action = () => RationalNumber.Parse(rationalNumber);
            action.Should().Throw<FormatException>();
        }

        [TestCase("-1_2/0")]
        [TestCase("1_1/0")]
        [TestCase("5/0")]
        [TestCase("-1/0")]
        public void ParseAnInvalidRationalNumberString(string rationalNumber)
        {
            Action action = () => RationalNumber.Parse(rationalNumber);
            action.Should().Throw<NotARationalNumberException>();
        }
    }
}
