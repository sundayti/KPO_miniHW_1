namespace ConsoleUtils.Interfaces;

/// <summary>
/// Интерфейс для ввода данных пользователем.
/// </summary>
public interface IInputService
{
    int ReadInt(int upperBound = Int32.MaxValue);
    double ReadDouble();
    string? ReadLine();
    ConsoleKeyInfo ReadKey();
}