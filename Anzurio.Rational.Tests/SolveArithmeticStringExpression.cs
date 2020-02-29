using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Anzurio.Rational.Tests
{
    [TestFixture]
    public class SolveArithmeticStringExpression
    {
        private static readonly string [] Operators = new string[] { " - ", " * ", " / ", " + " };

        [TestCase("2/3 / 0")]
        [TestCase("2/3 / 0/1")]
        [TestCase("-2/3 / 0/1")]
        [TestCase("3 / 0_0/1")]
        [TestCase("-1_1/1 / -0_0/1")]
        public void AttemptToDivideByZero(string expression)
        {
            Func<RationalNumber> fx = () => RationalNumber.SolveArithmeticExpression(expression);
            fx.Should().Throw<DivideByZeroException>();
        }

        [TestCase("_1/3")]
        [TestCase("3_")]
        [TestCase("/3")]
        [TestCase("2/")]
        [TestCase("5_2/")]
        [TestCase("3_ 2/3")]
        [TestCase("3 _2/3")]
        [TestCase("10 /4")]
        [TestCase("2/-3")]
        [TestCase("3_-1/3")]
        [TestCase("-1_-2/2")]
        [TestCase("1_-2/-2")]
        [TestCase("-2/-2")]
        [TestCase("-2/-2")]
        [TestCase("1/0")]
        [TestCase("A00")]
        [TestCase("00A")]
        public void AttemptToSolveWithInvalidOperand(string operand)
        {
            foreach (var op in Operators)
            {
                var forwardExpression = $"{operand}{op}1/3";
                var backwardsExpression = $"1/3{op}{operand}";
                Func<RationalNumber> ff = () => RationalNumber.SolveArithmeticExpression(forwardExpression);
                Func<RationalNumber> fb = () => RationalNumber.SolveArithmeticExpression(backwardsExpression);
                ff.Should().Throw<InvalidOperationException>().And.Message.Contains("left").Should().BeTrue();
                fb.Should().Throw<InvalidOperationException>().And.Message.Contains("right").Should().BeTrue();
            }
        }

        [TestCase("1-3")]
        [TestCase("3 -3")]
        [TestCase("3- 3")]
        [TestCase("3 * 4 - 4")]
        [TestCase("something")]
        public void AttemptToSolveAnInvalidExpression(string expression)
        {
            Func<RationalNumber> fx = () => RationalNumber.SolveArithmeticExpression(expression);
            fx.Should().Throw<InvalidOperationException>();
        }

        [TestCase("")]
        public void SolveValidExpression()
        {

        }
    }
}
