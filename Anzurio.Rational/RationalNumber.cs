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
            if (!ContainsAtMostOneNegativeNumber(new []{whole, numerator, denominator }))
            {
                throw new InvalidRationalNumber();
            }
        }

        public RationalNumber(int numerator, int denominator)
            : this(0, numerator, denominator)
        {

        }

        public RationalNumber(int whole) 
            : this(whole, 1)
        {

        }

        private static bool ContainsAtMostOneNegativeNumber(IEnumerable<int> numbers)
        {
            return numbers.Count(n => n < 0) <= 1;
        }
    }
}
