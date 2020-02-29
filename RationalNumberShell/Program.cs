using System;

namespace RationalNumberShell
{
    public class Program
    {
        private const string ExitCommand = "exit";
        private const string HelpCommand = "help";
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Rational Number Shell.");
            Console.WriteLine($"To exit the application, enter {ExitCommand}");
            Console.WriteLine($"To display instructions, enter {HelpCommand}");
            do
            {
                Console.Write("? ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                var trimmedLowedTextEntered = line.Trim().ToLower();
                if (trimmedLowedTextEntered == ExitCommand)
                {
                    break;
                }
                if (trimmedLowedTextEntered == HelpCommand)
                {
                    DisplayHelp();
                    continue;
                }
            } while (true);

        }

        private static void DisplayHelp()
        {
            Console.WriteLine("This application solves arithmetic expressions between two fractions.");
            Console.WriteLine("Supported operators are: + - * /. They must be surrounded by at least one space");
            Console.WriteLine("Supported fractions format:");
            Console.WriteLine("- Whole Numbers (e.g., 0, 1, -0, -10, 0005)");
            Console.WriteLine("- Improper Fractions (e.g., 7/5, -11/7)");
            Console.WriteLine("- Proper Fractions (e.g., 1/2, -2/3");
            Console.WriteLine("- Whole Number plus Proper or Improper Fraction (e.g., 0_0/1, -0_5/3, 1_7/5)");
            Console.WriteLine("Examples of invalid formats:");
            Console.WriteLine("- Underscore without a whole number preceding it (e.g., _1/3)");
            Console.WriteLine("- Underscore without a fraction number following it (e.g., 3_)");
            Console.WriteLine("- Missing either numerator or denominator of a fraction (e.g., /3, 2/, 5_2/");
            Console.WriteLine("- Spaces (e.g., 3_ 2/3, 3 _2/3, 10 / 4)");
            Console.WriteLine("- Minus sign anywhere except at the start of the string (e.g., 2/-3, 3_-1/3");
        }
    }
}
