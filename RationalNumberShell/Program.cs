using Anzurio.Rational;
using System;

namespace RationalNumberShell
{
    public class Program
    {
        private const string ExitCommand = "exit";
        private const string HelpCommand = "help";
        private const int AttemptsToDisplayHelp = 5;
        static void Main(string[] args)
        {
            int attempts = 0;
            Console.WriteLine("Welcome to the Rational Number Shell.");
            Console.WriteLine($"To exit the application, enter {ExitCommand}");
            Console.WriteLine($"To display instructions, enter {HelpCommand}");
            do
            {
                Console.Write("? ");
                var line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                {
                    attempts++;
                }
                else
                {
                    var trimmedLowedTextEntered = line.Trim().ToLower();
                    if (trimmedLowedTextEntered == ExitCommand)
                    {
                        break;
                    }
                    if (trimmedLowedTextEntered == HelpCommand)
                    {
                        DisplayHelp();
                        attempts = 0;
                        continue;
                    }
                    try
                    {
                        Console.WriteLine(RationalNumber.SolveArithmeticExpression(line).ToString());
                        attempts = 0;
                    }
                    catch (Exception ex)
                    {
                        attempts++;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(ex.Message);
                        Console.ResetColor();

                    }
                    if (attempts >= AttemptsToDisplayHelp)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("This information might be helpful:");
                        DisplayHelp();
                        attempts = 0;
                    }
                }
            } while (true);

        }

        private static void DisplayHelp()
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
            Console.WriteLine("- Spaces (e.g., 3_ 2/3, 3 _2/3, 10 /4)");
            Console.WriteLine("- Minus sign anywhere except at the start of the string (e.g., 2/-3, 3_-1/3");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Remarks:");
            Console.WriteLine("For consistency to other Parse methods in .NET-based shells, 0_0/3 and 0_1/3 are valid representations of a fractions (e.g., double.Parse(\"0.0\");)");
            Console.WriteLine("To avoid ambiguity, the minus sign to represent a negative number, must always be at the beginning of the fraction.");
            Console.WriteLine("Using a whole number and an improper fraction is valid as  3_2/3 is mathematically equivalent to 3 + 2/3 thus so would be 3_4/3.</p>");
            Console.ResetColor();
        }
    }
}
