using System;
using System.Collections.Generic;
using System.Text;

namespace Anzurio.Rational
{
    public class NotARationalNumberException : Exception
    {

        public NotARationalNumberException() : base("Zero is not a valid denominator value.")
        {

        }
    }
}
