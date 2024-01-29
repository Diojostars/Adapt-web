using System;
using System.IO;

class Program
{
	static void Main()
	{
		while (true)
		{
			Console.WriteLine("Choose an option:");
			Console.WriteLine("1. Count the number of words in the text");
			Console.WriteLine("2. Perform a mathematical operation");
			Console.WriteLine("3. Exit");

			Console.Write("Enter the option number: ");
			string input = Console.ReadLine();

			switch (input)
			{
				case "1":
					CountWords();
					break;
				case "2":
					PerformMathOperation();
					break;
				case "3":
					Console.WriteLine("Thank you for using the program. Goodbye!");
					return;
				default:
					Console.WriteLine("Invalid choice. Please try again.");
					break;
			}

			Console.WriteLine("\nPress any key to continue...");
			Console.ReadKey();
			Console.Clear();
		}
	}

	static void CountWords()
	{
		// Read text from a file
		string loremText = File.ReadAllText("LoremIpsum.txt");

		// Count the number of words
		int wordCount = loremText.Split(new char[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries).Length;

		Console.WriteLine($"Number of words in the text: {wordCount}");
	}

	static void PerformMathOperation()
	{
		Console.Write("Enter a mathematical expression to evaluate: ");
		string expression = Console.ReadLine();

		try
		{
			// Perform the mathematical operation
			double result = EvaluateMathExpression(expression);
			Console.WriteLine($"Result: {result}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error performing the operation: {ex.Message}");
		}
	}

	static double EvaluateMathExpression(string expression)
	{
		var dataTable = new System.Data.DataTable();
		var result = dataTable.Compute(expression, "");
		return Convert.ToDouble(result);
	}
}
