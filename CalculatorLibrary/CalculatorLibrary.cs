using System.Diagnostics;
using Newtonsoft.Json;

namespace CalculatorLibrary
{
    public class Calculator
    {
        JsonWriter writer;
        public List<OperationRecord> history = new();
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
        public double PerformOperation(string op, double num1, double num2 = 0.0)
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
                case "p":
                    res = Math.Pow(num1, num2);
                    writer.WriteValue("Power");
                    break;
                case "sr":
                    res = Math.Sqrt(num1);
                    writer.WriteValue("Square root");
                    break;
                case "ten":
                    res = Math.Pow(10, num1);
                    writer.WriteValue("10^x");
                    break;
                case "sin":
                    res = Math.Sin(num1);
                    writer.WriteValue("Sin");
                    break;
                case "cos":
                    res = Math.Cos(num1);
                    writer.WriteValue("Cos");
                    break;
                case "tan":
                    res = Math.Tan(num1);
                    writer.WriteValue("Tan");
                    break;

            }
            history.Add(new OperationRecord
            {
                Operand1 = num1,
                Operand2 = num2,
                Operation = op,
                Result = res
            });
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

        public void FetchHistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No history available yet.");
                return;
            }

            Console.WriteLine("Calculator History:");
            Console.WriteLine("-------------------");

            for (int i = 0; i < history.Count; i++)
            {
                OperationRecord record = history[i];

                if (new List<string>() { "a", "d", "m", "d", "p" }.Contains(record.Operation))
                    Console.WriteLine(
                        $"{i + 1}. {record.Operation}: " +
                        $"{record.Operand1}, {record.Operand2} = {record.Result}"
                    );
                else
                    Console.WriteLine(
                        $"{i + 1}. {record.Operation}: " +
                        $"{record.Operand1} = {record.Result}"
                    );
            }
        }
    }

    public class OperationRecord
    {
        public double Operand1 { get; set; }
        public double? Operand2 { get; set; }
        public string Operation { get; set; } = "";
        public double Result { get; set; }
    }
}
