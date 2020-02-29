using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Anzurio.Rational
{
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

        public int Numerator { get; internal set; }
        public int Denominator { get; internal set; }

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

        public RationalNumber(int numerator, int denominator)
            : this(0, numerator, denominator)
        {

        }

        public RationalNumber(int whole) 
            : this(whole, 1)
        {

        }
        
        public override string ToString()
        {
            var absoluteNumerator = Math.Abs(Numerator);
            if (Denominator == 1 || Numerator == 0 || absoluteNumerator < Denominator)
            {
                return ToImproperFractionString();
            }
            else
            {
                var sign = Numerator < 0 ? "-" : string.Empty;
                var whole = $"{absoluteNumerator / Denominator}";
                var remainder = absoluteNumerator % Denominator;
                return $"{sign}{whole}_{remainder}/{Denominator}";
            }
        }

        public string ToImproperFractionString()
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

        public static RationalNumber operator*(RationalNumber lhs, RationalNumber rhs)
        {
            return new RationalNumber(lhs.Numerator * rhs.Numerator, lhs.Denominator * rhs.Denominator);
        }

        public static RationalNumber operator/(RationalNumber lhs, RationalNumber rhs)
        {
            if (rhs.Numerator == 0)
            {
                throw new DivideByZeroException();
            }
            var areBothSidesNegative = lhs.Numerator < 0 && rhs.Numerator < 0;

            return new RationalNumber(
                lhs.Numerator * rhs.Denominator * (areBothSidesNegative ? -1 : 1), 
                lhs.Denominator * rhs.Numerator * (areBothSidesNegative ? -1 : 1));
        }

        public static RationalNumber operator +(RationalNumber lhs, RationalNumber rhs)
        {
            return new RationalNumber(
                (lhs.Numerator * rhs.Denominator) + (lhs.Denominator * rhs.Numerator),
                lhs.Denominator * rhs.Denominator);
        }

        public static RationalNumber operator -(RationalNumber lhs, RationalNumber rhs)
        {
            return new RationalNumber(
                (lhs.Numerator * rhs.Denominator) - (lhs.Denominator * rhs.Numerator),
                lhs.Denominator * rhs.Denominator);
        }

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

        public static int CalculateGreatestCommonFactor(int numerator, int denominator)
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
    }
}
