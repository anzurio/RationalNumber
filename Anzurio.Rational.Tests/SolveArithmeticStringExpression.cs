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
        public void AttemptToDivideByZeroShouldThrowDivideByZeroException(string expression)
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
        public void AttemptToSolveWithInvalidOperandShouldThrowInvalidOperationException(string operand)
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
        public void AttemptToSolveAnInvalidExpressionShouldThrowInvalidOperationException(string expression)
        {
            Func<RationalNumber> fx = () => RationalNumber.SolveArithmeticExpression(expression);
            fx.Should().Throw<InvalidOperationException>();
        }

        [TestCase("1 * 1/2", "1/2")]
        [TestCase("-1 * 1/2", "-1/2")]
        [TestCase("1 * -1/2", "-1/2")]
        [TestCase("-1/2 * -1/2", "1/4")]
        [TestCase("1/2 * 1/2", "1/4")]
        [TestCase("-1/2 * 1/2", "-1/4")]
        [TestCase("-5_1/2 * 1/2", "-2_3/4")]
        [TestCase("-5_1/2 * -1/2", "2_3/4")]
        [TestCase("5_1/2 * -1/2", "-2_3/4")]
        [TestCase("3/9 * 9", "3")]
        [TestCase("-9 * 3/9", "-3")]
        [TestCase("0 * 1/2", "0")]
        [TestCase("-0/4 * 1/2", "0")]
        [TestCase("-0/4 * -1/2", "0")]
        [TestCase("    -0/4    *     -0/2     ", "0")]
        [TestCase("1/100 * 100/1", "1")]
        [TestCase("-1/100 * 100/1", "-1")]
        [TestCase("2 * 5", "10")]
        [TestCase("-2 * 5", "-10")]
        [TestCase("1/7 * 1/5", "1/35")]
        [TestCase("1/7 * -1/5", "-1/35")]
        [TestCase("10_1/10 * 1_5/10", "15_3/20")]
        [TestCase("10_1/10 * -1_5/10", "-15_3/20")]
        [TestCase("1 / 1/2", "2")]
        [TestCase("-1 / 1/2", "-2")]
        [TestCase("1 / -1/2", "-2")]
        [TestCase("-1/2 / -1/2", "1")]
        [TestCase("1/2 / 1/2", "1")]
        [TestCase("  -1/2    /    1/2", "-1")]
        [TestCase("-5_1/2 / 1/2", "-11")]
        [TestCase("-5_1/2 / -1/2", "11")]
        [TestCase("5_1/2 / -1/2", "-11")]
        [TestCase("3/9 / 9", "1/27")]
        [TestCase("-9 / 3/9", "-27")]
        [TestCase("0 / 1/2", "0")]
        [TestCase("-0/4     /     1/2", "0")]
        [TestCase("-0/4 / -1/2", "0")]
        [TestCase("2 / 5", "2/5")]
        [TestCase("-2 / 5", "-2/5")]
        [TestCase("1/7 / 1/5", "5/7")]
        [TestCase("1/7 / -1/5", "-5/7")]
        [TestCase("10_1/10 / 1_5/10", "6_11/15")]
        [TestCase("10_1/10 / -1_5/10", "-6_11/15")]
        [TestCase("1 + 1/2", "1_1/2")]
        [TestCase("-1 + 1/2", "-1/2")]
        [TestCase("1  +  -1/2", "1/2")]
        [TestCase("-1/2 +    -1/2   ", "-1")]
        [TestCase("    1/2 + 1/2", "1")]
        [TestCase("-1/2 + 1/2", "0")]
        [TestCase("-5_1/2 + 1/2", "-5")]
        [TestCase("-5_1/2 + -1/2", "-6")]
        [TestCase("5_1/2 + -1/2", "5")]
        [TestCase("3/9 + 9", "9_1/3")]
        [TestCase("-9 + 3/9", "-8_2/3")]
        [TestCase("0 + 1/2", "1/2")]
        [TestCase("-0/4 + 1/2", "1/2")]
        [TestCase("-0/4 + -1/2", "-1/2")]
        [TestCase("-0/4 + -0/2", "0")]
        [TestCase("1/100 + 100/1", "100_1/100")]
        [TestCase("-1/100   +    100/1", "99_99/100")]
        [TestCase("2 + 5", "7")]
        [TestCase("-2 + 5", "3")]
        [TestCase("1/7 + 1/5", "12/35")]
        [TestCase("1/7 + -1/5", "-2/35")]
        [TestCase("10_1/10 + 1_5/10", "11_3/5")]
        [TestCase("10_1/10 + -1_5/10", "8_3/5")]
        [TestCase("1 - 1/2", "1/2")]
        [TestCase("  -1 - 1/2", "-1_1/2")]
        [TestCase("   1 -    -1/2   ", "1_1/2")]
        [TestCase("-1/2 - -1/2", "0")]
        [TestCase("1/2 - 1/2", "0")]
        [TestCase("-1/2 - 1/2", "-1")]
        [TestCase("-5_1/2 - 1/2", "-6")]
        [TestCase("-5_1/2 -   -1/2", "-5")]
        [TestCase("5_1/2 - -1/2", "6")]
        [TestCase("3/9 - 9", "-8_2/3")]
        [TestCase("-9 -   3/9", "-9_1/3")]
        [TestCase("0 - 1/2", "-1/2")]
        [TestCase("-0/4 - 1/2", "-1/2")]
        [TestCase("-0/4 - -1/2", "1/2")]
        [TestCase("-0/4 - -0/2", "0")]
        [TestCase("1/100 - 100/1", "-99_99/100")]
        [TestCase("-1/100 - 100/1", "-100_1/100")]
        [TestCase("2 - 5", "-3")]
        [TestCase("-2 - 5", "-7")]
        [TestCase("1/7 - 1/5", "-2/35")]
        [TestCase("1/7 -      -1/5", "12/35")]
        [TestCase("10_1/10 - 1_5/10", "8_3/5")]
        [TestCase("10_1/10 - -1_5/10", "11_3/5")]
        [TestCase("2147483647 + 1", "2147483648")]
        [TestCase("-2147483648 - 1", "-2147483649")]
        [TestCase("2147483647 * 2147483647/2147483647", "2147483647")]
        [TestCase("-2147483648 * -2147483647/2147483647", "2147483648")]
        [TestCase("2147483647 * -2147483647/2147483647", "-2147483647")]
        [TestCase("2147483647 / 2147483647/2147483647", "2147483647")]
        [TestCase("-2147483648 / -2147483647/2147483647", "2147483648")]
        [TestCase("2147483647 / -2147483647/2147483647", "-2147483647")]
        [TestCase("2147483647 * -2", "-4294967294")]
        [TestCase("-2147483648 / 1/2", "-4294967296")]
        public void SolveValidExpression(string expression, string desiredOutput)
        {
            RationalNumber.SolveArithmeticExpression(expression).ToString()
                .Should()
                .Be(desiredOutput);
        }

        [TestCase("2147483647 + 1")]
        [TestCase("-2147483648 - 1")]
        [TestCase("2147483647 * 2147483647/1")]
        [TestCase("-2147483648 * -2147483647/1")]
        [TestCase("2147483647 * -2147483647/1")]
        [TestCase("-2147483648 / 1/2")]
        [TestCase("2147483647 / 1/2")]
        public void AttemptToSolveRationalNumberExpressionWhoseResultIsOutsideInt32LimitsShouldNotThrow(string expression)
        {
            Func<RationalNumber> fx = () => RationalNumber.SolveArithmeticExpression(expression);
            fx.Should().NotThrow();
        }
    }
}
