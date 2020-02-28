using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anzurio.Rational
{
    public sealed class RationalNumber
    {
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
            throw new NotImplementedException();
        }

        public string ToImproperFractionString()
        {
            throw new NotImplementedException();
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

        private static int CountNegativeNumbers(IEnumerable<int> numbers)
        {
            return numbers.Count(n => n < 0);
        }
    }
}
