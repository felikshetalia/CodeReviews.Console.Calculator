using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {

        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculator.log");
            Trace.Listeners.Add(new TextWriterTraceListener(logFile));
            Trace.AutoFlush = true;
            Trace.WriteLine("Starting Calculator Log");
            Trace.WriteLine(String.Format("Started {0}", System.DateTime.Now.ToString()));
        }
        public double PerformOperation(double num1, double num2, string op)
        {
            double res = double.NaN;

            switch (op)
            {
                case "a":
                    res = num1 + num2;
                    Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, res));
                    break;
                case "s":
                    res = num1 - num2;
                    Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, res));
                    break;
                case "m":
                    res = num1 * num2;
                    Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, res));
                    break;
                case "d":
                    try
                    {
                        if (num2 == 0) throw new DivideByZeroException();
                        res = num1 / num2;
                        Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, res));
                    }
                    catch (DivideByZeroException)
                    {
                        while (num2 == 0)
                        {
                            System.Console.WriteLine("You can't divide by zero\n Enter another number that's not zero.");
                            num2 = Convert.ToDouble(Console.ReadLine());
                        }
                        res = num1 / num2;
                        Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, res));
                    }
                    break;
            }
            return res;
        }
    }
}
