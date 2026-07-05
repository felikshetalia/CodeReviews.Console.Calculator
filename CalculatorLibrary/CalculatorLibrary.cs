using System.Diagnostics;
using Newtonsoft.Json;  

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public Calculator()
        {
            StreamWriter logFile = File.CreateText("calculatorlog.json");
            logFile.AutoFlush = true;
            writer = new JsonTextWriter(logFile);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartObject();
            writer.WritePropertyName("Operations");
            writer.WriteStartArray();
        }
        public double PerformOperation(double num1, double num2, string op)
        {
            double res = double.NaN;
            writer.WriteStartObject();
            writer.WritePropertyName("Operand1");
            writer.WriteValue(num1);
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
            writer.WritePropertyName("Operation");

            switch (op)
            {
                case "a":
                    res = num1 + num2;
                    //Trace.WriteLine(String.Format("{0} + {1} = {2}", num1, num2, res));
                    writer.WriteValue("Add");
                    break;
                case "s":
                    res = num1 - num2;
                    //Trace.WriteLine(String.Format("{0} - {1} = {2}", num1, num2, res));
                    writer.WriteValue("Subtract");
                    break;
                case "m":
                    res = num1 * num2;
                    //Trace.WriteLine(String.Format("{0} * {1} = {2}", num1, num2, res));
                    writer.WriteValue("Multiply");
                    break;
                case "d":
                    try
                    {
                        if (num2 == 0) throw new DivideByZeroException();
                        res = num1 / num2;
                        //Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, res));
                        writer.WriteValue("Divide");
                    }
                    catch (DivideByZeroException)
                    {
                        while (num2 == 0)
                        {
                            System.Console.WriteLine("You can't divide by zero\n Enter another number that's not zero.");
                            num2 = Convert.ToDouble(Console.ReadLine());
                        }
                        res = num1 / num2;
                        //Trace.WriteLine(String.Format("{0} / {1} = {2}", num1, num2, res));
                        writer.WriteValue("Divide");
                    }
                    break;
            }
            writer.WritePropertyName("Result");
            writer.WriteValue(res);
            writer.WriteEndObject();
            return res;
        }
        public void Finish()
        {
            writer.WriteEndArray();
            writer.WriteEndObject();
            writer.Close();
        }

    }
}
