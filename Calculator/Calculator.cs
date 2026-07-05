namespace CodeReviews_Console_Calculator;

internal class Calculator
{
    public static double PerformOperation(double num1, double num2, string op)
    {
        double res = double.NaN;

        switch (op)
        {
            case "a":
                res = num1 + num2;
                break;
            case "s":
                res = num1 - num2;
                break;
            case "m":
                res = num1 * num2;
                break;
            case "d":
                try
                {
                    if (num2 == 0) throw new DivideByZeroException();
                    res = num1 / num2;
                }
                catch (DivideByZeroException)
                {
                    while (num2 == 0)
                    {
                        System.Console.WriteLine("You can't divide by zero\n Enter another number that's not zero.");
                        num2 = Convert.ToDouble(Console.ReadLine());
                    }
                    res = num1 / num2;
                }
                break;
        }
        return res;
    }
}