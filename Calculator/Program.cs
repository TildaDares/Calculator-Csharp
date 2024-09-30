﻿using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorProgram;
class Program
{
    private static readonly Calculator calculator = new Calculator();
    static void Main(string[] args)
    {
        bool endApp = false;
        // Display title as the C# console calculator app.
        Console.WriteLine("Console Calculator in C#\r");
        Console.WriteLine("------------------------\n");
        
        while (!endApp)
        {
            // Declare variables and set to empty.
            // Use Nullable types (with ?) to match type of System.Console.ReadLine
            string? numInput1 = "";
            string? numInput2 = "";
            double result = 0;

            // Ask the user to type the first number.
            Console.Write("Type a number, and then press Enter: ");
            numInput1 = Console.ReadLine();

            double cleanNum1 = 0;
            while (!double.TryParse(numInput1, out cleanNum1))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput1 = Console.ReadLine();
            }

            // Ask the user to type the second number.
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNB: Second number will be ignored for unary operations.");
            Console.ResetColor();
            Console.Write("Type another number, and then press Enter: ");
            numInput2 = Console.ReadLine();

            double cleanNum2 = 0;
            while (!double.TryParse(numInput2, out cleanNum2))
            {
                Console.Write("This is not valid input. Please enter a numeric value: ");
                numInput2 = Console.ReadLine();
            }

            // Ask the user to choose an operator.
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.WriteLine("\tr - Square Root");
            Console.WriteLine("\tp - Raised to the power of");
            Console.WriteLine("\tt - Multiply by 10");
            Console.WriteLine("\tsin - Sine");
            Console.WriteLine("\tcos - Cosine");
            Console.WriteLine("\ttan - Tangent");
            Console.Write("Your option? ");

            int calculatorCount = 0;
            string? op = Console.ReadLine();

            // Validate input is not null, and matches the pattern
            if (op == null || ! Regex.IsMatch(op, "[a|s|m|d|r|p|t|sin|cos|tan]"))
            {
               Console.WriteLine("Error: Unrecognized input.");
            }
            else
            { 
               try
               {
                  result = calculator.DoOperation(cleanNum1, cleanNum2, op);
                  calculatorCount++;
                  if (double.IsNaN(result))
                  {
                     Console.WriteLine("This operation will result in a mathematical error.\n");
                  }
                  else Console.WriteLine("Your result: {0:0.##}\n", result);
               }
               catch (Exception e)
               {
                   Console.WriteLine("Oh no! An exception occurred trying to do the math.\n - Details: " + e.Message);
               }
            }
            Console.WriteLine("------------------------\n");

            // Wait for the user to respond before closing.
            Console.WriteLine("Select an option:");
            Console.WriteLine("Press any key and Enter to continue:");
            Console.WriteLine("\th - View history");
            Console.WriteLine("\tc - Clear history");
            Console.WriteLine("\tn - Quit");
            var userInput = Console.ReadLine();

            switch (userInput)
            {
                case "h":
                    ViewCalculationHistory();
                    break;
                case "c":
                    calculator.ClearCalculatorHistory();
                    Console.WriteLine("History cleared!\n");
                    break;
                case "n":
                    Console.WriteLine($"You used the calculator {calculatorCount} times!");
                    endApp = true;
                    break;
            }

            Console.WriteLine("\n");
        }
        calculator.Finish();
    }

    public static void ViewCalculationHistory()
    {
        var calculations = calculator.Operations;
        Console.Clear();
        Console.WriteLine("\nCalculation History:");
        foreach (var calc in calculations)
        {
            Console.WriteLine($"{calc.Item1} = {calc.Item2}");
        }
    }
}