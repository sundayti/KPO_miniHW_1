using ConsoleUtils.Interfaces;

namespace ConsoleUtils;

/// <summary>
/// Сервис для ввода данных с консоли.
/// </summary>
public class ConsoleInputService(IOutputService output) : IInputService
{
    // Внедрение IOutputService через конструктор.

    public int ReadInt(int upperBound = Int32.MaxValue)
    {
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0 || n > upperBound)
        {
            output.WriteLine("Invalid data! Please try again.", ConsoleColor.Red);
        }
        return n;
    }

    public double ReadDouble()
    {
        double n;
        while (!double.TryParse(Console.ReadLine()?.Replace('.', ','), out n))
        {
            output.WriteLine("Invalid data! Please try again.", ConsoleColor.Red);
        }
        return n;
    }

    public string? ReadLine() => Console.ReadLine();

    public ConsoleKeyInfo ReadKey() => Console.ReadKey();
}