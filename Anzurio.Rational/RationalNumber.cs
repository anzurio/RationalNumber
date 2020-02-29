using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Anzurio.Rational
{
    /// <summary>
    /// A class that represents a Rational Number.
    /// </summary>
    public sealed class RationalNumber
    {
        private const string WholeOnlyGroupId = "wholeonly";
        private const string FractionOnlyGroupId = "fractiononly";
        private const string MixedGroupId = "mixed";
        private const string LeftOperandGroupId = "lhs";
        private const string RightOperandGroupId = "rhs";
        private const string OperatorGroupId = "operator";

        private static readonly string RegularExpressionPattern = 
            $@"^((?<{WholeOnlyGroupId}>-?\d+)|(?<{FractionOnlyGroupId}>-?\d+/\d+)|(?<{MixedGroupId}>-?\d+_\d+/\d+))$";
        private static readonly string RegularExpressionArithmeticExpressionPattern =
            $"(?<{LeftOperandGroupId}>^.+)(?<{OperatorGroupId}>" + 
            @"\s{1}[-/\+\*]\s{1})" +
            $"(?<{RightOperandGroupId}>.+$)";

        /// <summary>
        /// The numerator of the rational number.
        /// </summary>
        public int Numerator { get; internal set; }
        /// <summary>
        /// The denominator of the rational number.
        /// </summary>
        public int Denominator { get; internal set; }

        /// <summary>
        /// Creates a RationalNumber by providing a whole number (which may be zero) and a fraction.
        /// </summary>
        /// <param name="whole">A whole number.</param>
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <exception cref="NotARationalNumberException"><paramref name="denominator"/> is zero.</exception>
        /// <exception cref="InvalidRationalNumber">More than one of the parameters is negative.</exception>
        public RationalNumber(int whole, int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new NotARationalNumberException();
            }
            var negativeNumbers = CountNegativeNumbers(new[] { whole, numerator, denominator });
            if (negativeNumbers > 1)
            {
                throw new InvalidRationalNumber();
            }
            whole = Math.Abs(whole);
            numerator = Math.Abs(numerator);
            denominator = Math.Abs(denominator);
            
            numerator += (whole * denominator);
            var greatestCommonFactor = CalculateGreatestCommonFactor(numerator, denominator);
            greatestCommonFactor = greatestCommonFactor == 0 ? 1 : greatestCommonFactor;

            Numerator = (numerator / greatestCommonFactor) * (negativeNumbers == 1 ? -1 : 1);
            Denominator = denominator / greatestCommonFactor;
        }

        /// <summary>
        /// Creates a RationalNumber by providing a fraction.
        /// </summary>
        /// <param name="numerator">The numerator of the fraction.</param>
        /// <param name="denominator">The denominator of the fraction.</param>
        /// <exception cref="NotARationalNumberException"><paramref name="denominator"/> is zero.</exception>
        /// <exception cref="InvalidRationalNumber">More than one of the parameters is negative.</exception>
        public RationalNumber(int numerator, int denominator)
            : this(0, numerator, denominator)
        {

        }

        /// <summary>
        /// Creates a RationalNumber by providing a whole number.
        /// </summary>
        /// <param name="whole">A whole number.</param>
        public RationalNumber(int whole) 
            : this(whole, 1)
        {

        }
        
        /// <summary>
        /// Returns a reduced proper fraction representation of this Rational Number.
        /// </summary>
        /// <returns>A string representing the reduced proper fraction of this Rational Number.</returns>
        public override string ToString()
        {
            var absoluteNumerator = Math.Abs(Numerator);
            if (Denominator == 1 || Numerator == 0 || absoluteNumerator < Denominator)
            {
                return ToRationalString();
            }
            else
            {
                var sign = Numerator < 0 ? "-" : string.Empty;
                var whole = $"{absoluteNumerator / Denominator}";
                var remainder = absoluteNumerator % Denominator;
                return $"{sign}{whole}_{remainder}/{Denominator}";
            }
        }

        /// <summary>
        /// Returns a string representation of this Rational Number.
        /// </summary>
        /// <returns>A string representing this Rational Number.</returns>
        public string ToRationalString()
        {
            if (Denominator == 1 || Numerator == 0)
            {
                return $"{Numerator}";
            }
            else
            {
                return $"{Numerator}/{Denominator}";
            }
        }

        /// <summary>
        /// Multiplies two Rational Numbers.
        /// </summary>
        /// <param name="lhs">The left hand side of the operation.</param>
        /// <param name="rhs">The right hand side of the operation.</param>
        /// <exception cref="ArgumentNullException">Either parameter is null.</exception>
        /// <returns>The result of the multiplication.</returns>
        public static RationalNumber operator*(RationalNumber lhs, RationalNumber rhs)
        {
            ValidateOperandsNotNullOrThrow(lhs, rhs);
            
            return new RationalNumber(lhs.Numerator * rhs.Numerator, lhs.Denominator * rhs.Denominator);
        }

        /// <summary>
        /// Divides two Rational Numbers.
        /// </summary>
        /// <param name="lhs">The left hand side of the operation.</param>
        /// <param name="rhs">The right hand side of the operation.</param>
        /// <exception cref="ArgumentNullException">Either parameter is null.</exception>
        /// <exception cref="DivideByZeroException">The <paramref name="rhs"/> is equivalent to zero.</exception>
        /// <returns>The result of the division.</returns>
        public static RationalNumber operator/(RationalNumber lhs, RationalNumber rhs)
        {
            ValidateOperandsNotNullOrThrow(lhs, rhs);
            if (rhs.Numerator == 0)
            {
                throw new DivideByZeroException();
            }
            var areBothSidesNegative = lhs.Numerator < 0 && rhs.Numerator < 0;

            return new RationalNumber(
                lhs.Numerator * rhs.Denominator * (areBothSidesNegative ? -1 : 1), 
                lhs.Denominator * rhs.Numerator * (areBothSidesNegative ? -1 : 1));
        }

        /// <summary>
        /// Sums two Rational Numbers.
        /// </summary>
        /// <param name="lhs">The left hand side of the operation.</param>
        /// <param name="rhs">The right hand side of the operation.</param>
        /// <exception cref="ArgumentNullException">Either parameter is null.</exception>
        /// <returns>The result of the sum.</returns>
        public static RationalNumber operator +(RationalNumber lhs, RationalNumber rhs)
        {
            ValidateOperandsNotNullOrThrow(lhs, rhs);
            return new RationalNumber(
                (lhs.Numerator * rhs.Denominator) + (lhs.Denominator * rhs.Numerator),
                lhs.Denominator * rhs.Denominator);
        }

        /// <summary>
        /// Substracts two Rational Numbers.
        /// </summary>
        /// <param name="lhs">The left hand side of the operation.</param>
        /// <param name="rhs">The right hand side of the operation.</param>
        /// <exception cref="ArgumentNullException">Either parameter is null.</exception>
        /// <returns>The result of the substraction.</returns>
        public static RationalNumber operator -(RationalNumber lhs, RationalNumber rhs)
        {
            ValidateOperandsNotNullOrThrow(lhs, rhs);
            return new RationalNumber(
                (lhs.Numerator * rhs.Denominator) - (lhs.Denominator * rhs.Numerator),
                lhs.Denominator * rhs.Denominator);
        }

        /// <summary>
        /// Converts the string representation of a fraction to an equivalent Rational Numbers.
        /// </summary>
        /// <param name="s">A string containing a fraction to convert.</param>
        /// <remarks>
        /// <p>For consistency to other Parse methods in .NET, 0_0/3 and 0_1/3 are valid representations of a fractions (e.g., <code>double.Parse("0.0");</code>).</p>
        /// <p>To avoid ambiguity, the minus sign to represent a negative number, must always be at the beginning of the fraction.</p>
        /// <p>Using a whole number and an improper fraction is valid as  3_2/3 is mathematically equivalent to 3 + 2/3 thus so would be 3_4/3.</p>
        /// </remarks>
        /// <returns>A Rational Number equivalent to the fraction specified in <paramref name="s"/></returns>
        /// <exception cref="ArgumentNullException"><paramref name="s"/> is null.</exception>
        /// <exception cref="FormatException"><paramref name="s"/> is not in a correct format.</exception>
        /// <exception cref="OverflowException">Any numeric element of <paramref name="s"/> is larger than the numeric bounds.</exception>
        public static RationalNumber Parse(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            var trimmedString = s.Trim();
            var regEx = new Regex(RegularExpressionPattern);
            var match = regEx.Match(trimmedString);
            if (match.Success)
            {
                return CreateRationalNumberFromRegexSuccessfulMatch(match);
            }
            else
            {
                throw new FormatException("Input string was not in a correct format.");
            }
        }

        /// <summary>
        /// Converts the string representation of a fraction to an equivalent Rational Numbers. 
        /// A return value indicates whether the operation succeeded. This method calls <see cref="RationalNumber.Parse(string)"/>;
        /// see that method documentation for more information.
        /// </summary>
        /// <param name="s">A string containing a fraction to convert.</param>
        /// <param name="result">If the conversion is successful, it contains the result of the conversion.</param>
        /// <returns>true if the conversion was successful.</returns>
        public static bool TryParse(string s, out RationalNumber result)
        {
            try
            {
                result = Parse(s);
                return true;
            }
            catch (Exception)
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Converts an Arithmetic operation between two and only two Rational Numbers and solves the operation.
        /// </summary>
        /// <param name="expression">The expression representing the arithmetic operation to be performed.</param>
        /// <returns>The resulting value of performing the operation.</returns>
        /// <exception cref="InvalidOperationException">A valid operator is not present or not surrounded by spaces.</exception>
        /// <exception cref="FormatException">Either operand is not in a correct format.</exception>
        /// <exception cref="OverflowException">Any numeric component of either operand is larger than the numeric bounds.</exception>
        /// <exception cref="DivideByZeroException">The right operand is equivalent to zero.</exception>
        public static RationalNumber SolveArithmeticExpression(string expression)
        {
            var regEx = new Regex(RegularExpressionArithmeticExpressionPattern);
            var match = regEx.Match(expression);
            if (match.Success)
            {
                var op = match.Groups[OperatorGroupId].Value;
                var (leftOperand, rightOperand) = GetOperandsFromRegexSuccessfulMatch(match);

#if NETCOREAPP3_0
                var result = op switch
                {
                    " - " => leftOperand - rightOperand,
                    " * " => leftOperand * rightOperand,
                    " / " => leftOperand / rightOperand,
                    _ => leftOperand + rightOperand
                };
#else
                RationalNumber result;
                switch (op)
                {
                    case " - ": result = leftOperand - rightOperand; break;
                    case " * ": result = leftOperand * rightOperand; break;
                    case " / ": result = leftOperand / rightOperand; break;
                    default: result = leftOperand + rightOperand; break;
                };
#endif
                return result;
            }
            else
            {
                throw new InvalidOperationException(
                    "One operator must be present and it must be surrounded by spaces.");
            }
        }

        private static int CalculateGreatestCommonFactor(int numerator, int denominator)
        {
            while (numerator != 0 && denominator != 0)
            {
                if (numerator > denominator)
                {
                    numerator %= denominator;
                }
                else
                {
                    denominator %= numerator;
                }
            }

            return Math.Max(denominator, numerator);
        }

        private static RationalNumber CreateRationalNumberFromRegexSuccessfulMatch(Match match)
        {
            if (match.Groups[WholeOnlyGroupId].Success)
            {
                return new RationalNumber(int.Parse(match.Groups[WholeOnlyGroupId].Value));
            }
            string wholeString = "0";
            string fractionString;
            if (match.Groups[MixedGroupId].Success)
            {
                var underscoreSplittedStrings = match.Groups[MixedGroupId].Value.Split('_');
                wholeString = underscoreSplittedStrings[0];
                fractionString = underscoreSplittedStrings[1];
            }
            else
            {
                fractionString = match.Groups[FractionOnlyGroupId].Value;
            }
            var slashSplittedStrings = fractionString.Split('/');
            var whole = int.Parse(wholeString);
            var numerator = int.Parse(slashSplittedStrings[0]);
            var denominator = int.Parse(slashSplittedStrings[1]);
            return new RationalNumber(whole, numerator, denominator);
        }

        private static (RationalNumber leftOperand, RationalNumber rightOperand) GetOperandsFromRegexSuccessfulMatch(Match match)
        {
            RationalNumber leftOperand, rightOperand;
            var lhs = match.Groups[LeftOperandGroupId].Value.Trim();
            var rhs = match.Groups[RightOperandGroupId].Value.Trim();
           
            try
            {
                leftOperand = Parse(lhs);
            }
            catch (Exception leftOperandException)
            {
                throw new InvalidOperationException(
                    $"Error on left operand \"{lhs}\": {Environment.NewLine}{leftOperandException.Message}");
            }

            try
            {
                rightOperand = Parse(rhs);
            }
            catch (Exception rightOperandException)
            {
                throw new InvalidOperationException(
                    $"Error on right operand \"{rhs}\": {Environment.NewLine}{rightOperandException.Message}");
            }

            return (leftOperand, rightOperand);
        }

        private static int CountNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Count(n => n < 0);
        }

        private static void ValidateOperandsNotNullOrThrow(RationalNumber lhs, RationalNumber rhs)
        {
            if (lhs == null)
            {
                throw new ArgumentNullException(nameof(lhs));
            }
            if (rhs == null)
            {
                throw new ArgumentNullException(nameof(rhs));
            }
        }
    }
}
