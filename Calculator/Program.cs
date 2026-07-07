using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using CalculatorLibrary;
namespace CodeReviews_Console_Calculator
{
    class Program
    {
        public const double pi = 3.14;
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            bool endApp = false;
            // Display title as the C# console calculator app.
            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");

            int usageCounter = 0;

            while (!endApp)
            {
                // Declare variables and set to empty.
                // Use Nullable types (with ?) to match type of System.Console.ReadLine
                string? numInput1 = "";
                string? numInput2 = "";
                double cleanNum1 = 0;
                double cleanNum2 = 0;
                double result = 0;

                ShowMenuLines();

                string? op = Console.ReadLine();

                // Validate input is not null, and matches the pattern
                if (op == null || !Regex.IsMatch(op, "^(a|s|m|d|p|sr|ten|sin|cos|tan|h|c)$"))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    if (op == "h")
                    {
                        calculator.FetchHistory();
                        continue;
                    }
                    if (op == "c")
                    {
                        calculator.history.Clear();
                        continue;
                    }

                    System.Console.WriteLine("------ SHORTCUTS ------");
                    System.Console.WriteLine("ans - Use the latest result");
                    System.Console.WriteLine("pi - 3.14\n");

                    Console.Write("Type a number, and then press Enter: ");
                    numInput1 = Console.ReadLine();

                    while (!double.TryParse(numInput1, out cleanNum1))
                    {
                        if (numInput1 == "ans")
                        {
                            try
                            {
                                cleanNum1 = calculator.history[calculator.history.Count - 1].Result;
                                break;

                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                Console.WriteLine("The history of results is empty. Make some calculations with real numbers first.");
                                System.Console.WriteLine("Enter a number then press enter");
                            }
                        }
                        else if (numInput1 == "pi")
                        {
                            cleanNum1 = pi;
                            break;
                        }
                        else
                            Console.Write("This is not valid input. Please enter a numeric value: ");
                        numInput1 = Console.ReadLine();
                    }
                    // if the operation takes 2, get another number
                    if (Regex.IsMatch(op, "^(a|s|m|d|p)$"))
                    {

                        Console.Write("Type another number, and then press Enter: ");
                        numInput2 = Console.ReadLine();

                        while (!double.TryParse(numInput2, out cleanNum2))
                        {
                            if (numInput2 == "ans")
                            {
                                try
                                {
                                    cleanNum2 = calculator.history[calculator.history.Count - 1].Result;
                                    break;
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    Console.WriteLine("The history of results is empty. Make some calculations with real numbers first.");
                                    System.Console.WriteLine("Enter a number then press enter");
                                }
                            }
                            else if (numInput2 == "pi")
                            {
                                cleanNum2 = pi;
                                break;
                            }
                            else
                                Console.Write("This is not valid input. Please enter a numeric value: ");
                            numInput2 = Console.ReadLine();
                        }
                    }
                    try
                    {
                        result = calculator.PerformOperation(op, cleanNum1, cleanNum2);
                        if (double.IsNaN(result))
                        {
                            Console.WriteLine("This operation will result in a mathematical error.\n");
                        }
                        else
                        {
                            Console.WriteLine("Your result: {0:0.##}\n", result);
                            usageCounter++;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
                    }
                }
                Console.WriteLine("------------------------");

                // Wait for the user to respond before closing.
                System.Console.WriteLine("Calculator usage: " + usageCounter);
                Console.Write("\nPress 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly linespacing.
            }
            calculator.Finish();
            return;
        }

        static void ShowMenuLines()
        {
            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tp - Power");
            // above operations take 2 arguments
            Console.WriteLine("\tsr - Square root");
            Console.WriteLine("\tten - 10^x");
            Console.WriteLine("\tsin - Sin");
            Console.WriteLine("\tcos - Cos");
            Console.WriteLine("\ttan - Tan");
            // above operations take 1
            System.Console.WriteLine("\n\th - Show history");
            System.Console.WriteLine("\tc - Clear history");

            Console.Write("\n\nYour option? ");
        }
    }
}
