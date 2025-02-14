using ConsoleUtils.Interfaces;

namespace ConsoleUtils;

/// <summary>
/// Сервис для вывода данных в консоль.
/// </summary>
public class ConsoleOutputService : IOutputService
{
    public void WriteLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public void Write(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ResetColor();
    }

    public void WriteError(string errorMessage)
    {
        Console.Clear();
        WriteLine(errorMessage, ConsoleColor.Red);
        Console.WriteLine("\nPress any key to go back.");
        Console.ReadKey();
    }

    public void OutputCatalog(string[] catalog)
    {
        for (int i = 0; i < catalog.Length; i++)
        {
            Console.Write($" {i + 1} - ");
            WriteLine(catalog[i], ConsoleColor.DarkCyan);
        }
    }
}