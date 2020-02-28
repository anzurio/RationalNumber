﻿using FluentAssertions;
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

    }
}