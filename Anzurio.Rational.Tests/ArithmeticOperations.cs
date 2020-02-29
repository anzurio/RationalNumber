using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;


namespace Anzurio.Rational.Tests
{
    [TestFixture]
    public class ArithmeticOperations
    {
        [TestCase("1", "1/2", "1/2")]
        [TestCase("-1", "1/2", "-1/2")]
        [TestCase("1", "-1/2", "-1/2")]
        [TestCase("-1/2", "-1/2", "1/4")]
        [TestCase("1/2", "1/2", "1/4")]
        [TestCase("-1/2", "1/2", "-1/4")]
        [TestCase("-5_1/2", "1/2", "-2_3/4")]
        [TestCase("-5_1/2", "-1/2", "2_3/4")]
        [TestCase("5_1/2", "-1/2", "-2_3/4")]
        [TestCase("3/9", "9", "3")]
        [TestCase("-9", "3/9", "-3")]
        [TestCase("0", "1/2", "0")]
        [TestCase("-0/4", "1/2", "0")]
        [TestCase("-0/4", "-1/2", "0")]
        [TestCase("-0/4", "-0/2", "0")]
        [TestCase("1/100", "100/1", "1")]
        [TestCase("-1/100", "100/1", "-1")]
        [TestCase("2", "5", "10")]
        [TestCase("-2", "5", "-10")]
        [TestCase("1/7", "1/5", "1/35")]
        [TestCase("1/7", "-1/5", "-1/35")]
        [TestCase("10_1/10", "1_5/10", "15_3/20")]
        [TestCase("10_1/10", "-1_5/10", "-15_3/20")]
        public void PerfomMultiplication(string leftHandSide, string rightHandSide, string desiredOutput)
        {
            var lhs = RationalNumber.Parse(leftHandSide);
            var rhs = RationalNumber.Parse(rightHandSide);
            (lhs * rhs).ToString().Should().Be(desiredOutput);
        }

        [TestCase("1", "1/2", "2")]
        [TestCase("-1", "1/2", "-2")]
        [TestCase("1", "-1/2", "-2")]
        [TestCase("-1/2", "-1/2", "1")]
        [TestCase("1/2", "1/2", "1")]
        [TestCase("-1/2", "1/2", "-1")]
        [TestCase("-5_1/2", "1/2", "-11")]
        [TestCase("-5_1/2", "-1/2", "11")]
        [TestCase("5_1/2", "-1/2", "-11")]
        [TestCase("3/9", "9", "1/27")]
        [TestCase("-9", "3/9", "-27")]
        [TestCase("0", "1/2", "0")]
        [TestCase("-0/4", "1/2", "0")]
        [TestCase("-0/4", "-1/2", "0")]
        [TestCase("2", "5", "2/5")]
        [TestCase("-2", "5", "-2/5")]
        [TestCase("1/7", "1/5", "5/7")]
        [TestCase("1/7", "-1/5", "-5/7")]
        [TestCase("10_1/10", "1_5/10", "6_11/15")]
        [TestCase("10_1/10", "-1_5/10", "-6_11/15")]
        public void PerfomDivision(string leftHandSide, string rightHandSide, string desiredOutput)
        {
            var lhs = RationalNumber.Parse(leftHandSide);
            var rhs = RationalNumber.Parse(rightHandSide);
            (lhs / rhs).ToString().Should().Be(desiredOutput);
        }

        [TestCase("1")]
        [TestCase("-1")]
        [TestCase("1_1/2")]
        [TestCase("-1_1/2")]
        [TestCase("3/2")]
        [TestCase("-3/2")]
        [TestCase("1/2")]
        [TestCase("-1/2")]
        public void AttemptToPerformDivisionByZero(string leftHandSide)
        {
            var lhs = RationalNumber.Parse(leftHandSide);
            var rhs = RationalNumber.Parse("0");
            Func<RationalNumber> fx = () => lhs / rhs;
            fx.Should().Throw<DivideByZeroException>();
        }

        [TestCase("1", "1/2", "1_1/2")]
        [TestCase("-1", "1/2", "-1/2")]
        [TestCase("1", "-1/2", "1/2")]
        [TestCase("-1/2", "-1/2", "-1")]
        [TestCase("1/2", "1/2", "1")]
        [TestCase("-1/2", "1/2", "0")]
        [TestCase("-5_1/2", "1/2", "-5")]
        [TestCase("-5_1/2", "-1/2", "-6")]
        [TestCase("5_1/2", "-1/2", "5")]
        [TestCase("3/9", "9", "9_1/3")]
        [TestCase("-9", "3/9", "-8_2/3")]
        [TestCase("0", "1/2", "1/2")]
        [TestCase("-0/4", "1/2", "1/2")]
        [TestCase("-0/4", "-1/2", "-1/2")]
        [TestCase("-0/4", "-0/2", "0")]
        [TestCase("1/100", "100/1", "100_1/100")]
        [TestCase("-1/100", "100/1", "99_99/100")]
        [TestCase("2", "5", "7")]
        [TestCase("-2", "5", "3")]
        [TestCase("1/7", "1/5", "12/35")]
        [TestCase("1/7", "-1/5", "-2/35")]
        [TestCase("10_1/10", "1_5/10", "11_3/5")]
        [TestCase("10_1/10", "-1_5/10", "8_3/5")]
        public void PerformSum(string leftHandSide, string rightHandSide, string desiredOutput)
        {
            var lhs = RationalNumber.Parse(leftHandSide);
            var rhs = RationalNumber.Parse(rightHandSide);
            (lhs + rhs).ToString().Should().Be(desiredOutput);
        }

        [TestCase("1", "1/2", "1/2")]
        [TestCase("-1", "1/2", "-1_1/2")]
        [TestCase("1", "-1/2", "1_1/2")]
        [TestCase("-1/2", "-1/2", "0")]
        [TestCase("1/2", "1/2", "0")]
        [TestCase("-1/2", "1/2", "-1")]
        [TestCase("-5_1/2", "1/2", "-6")]
        [TestCase("-5_1/2", "-1/2", "-5")]
        [TestCase("5_1/2", "-1/2", "6")]
        [TestCase("3/9", "9", "-8_2/3")]
        [TestCase("-9", "3/9", "-9_1/3")]
        [TestCase("0", "1/2", "-1/2")]
        [TestCase("-0/4", "1/2", "-1/2")]
        [TestCase("-0/4", "-1/2", "1/2")]
        [TestCase("-0/4", "-0/2", "0")]
        [TestCase("1/100", "100/1", "-99_99/100")]
        [TestCase("-1/100", "100/1", "-100_1/100")]
        [TestCase("2", "5", "-3")]
        [TestCase("-2", "5", "-7")]
        [TestCase("1/7", "1/5", "-2/35")]
        [TestCase("1/7", "-1/5", "12/35")]
        [TestCase("10_1/10", "1_5/10", "8_3/5")]
        [TestCase("10_1/10", "-1_5/10", "11_3/5")]
        public void PerformSubstraction(string leftHandSide, string rightHandSide, string desiredOutput)
        {
            var lhs = RationalNumber.Parse(leftHandSide);
            var rhs = RationalNumber.Parse(rightHandSide);
            (lhs - rhs).ToString().Should().Be(desiredOutput);
        }
    }
}
