using Newtonsoft.Json;
using System.Diagnostics;
namespace CalculatorLibrary;

public class Calculator
{
    JsonWriter writer;
    private string[] unaryOperations = new string[] { "r", "t", "sin", "cos", "tan" }; // Square root, Multiply by 10 (10x), Sin, Cos, Tan

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
    
    // CalculatorLibrary.cs
    public double DoOperation(double num1, double num2, string op)
    {
        double result = double.NaN; // Default value is "not-a-number" if an operation, such as division, could result in an error.
        writer.WriteStartObject();
        writer.WritePropertyName("Operand1");
        writer.WriteValue(num1);
        if (!unaryOperations.Contains(op))
        {
            writer.WritePropertyName("Operand2");
            writer.WriteValue(num2);
        }
        writer.WritePropertyName("Operation");
        // Use a switch statement to do the math.
        switch (op)
        {
            case "a":
                result = num1 + num2;
                writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                writer.WriteValue("Multiply");
                break;
            case "d":
                // Ask the user to enter a non-zero divisor.
                if (num2 != 0)
                {
                    result = num1 / num2;
                }
                writer.WriteValue("Divide");
                break;
            case "r":
                result = Math.Sqrt(num1);
                writer.WriteValue("Square Root");
                break;
            case "p":
                result = Math.Pow(num1, num2);
                writer.WriteValue("Power of");
                break;
            case "t":
                result = num1 * 10;
                writer.WriteValue("10x");
                break;
            case "sin":
                result = Math.Sin(num1);
                writer.WriteValue("Sine");
                break;
            case "cos":
                result = Math.Cos(num1);
                writer.WriteValue("Cosine");
                break;
            case "tan":
                result = Math.Tan(num1);
                writer.WriteValue("Tangent");
                break;
            // Return text for an incorrect option entry.
            default:
                break;
        }
        writer.WritePropertyName("Result");
        writer.WriteValue(result);
        writer.WriteEndObject();

        return result;
    }
    
    // CalculatorLibrary.cs
    public void Finish()
    {
        writer.WriteEndArray();
        writer.WriteEndObject();
        writer.Close();
    }
}