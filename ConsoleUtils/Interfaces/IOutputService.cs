namespace ConsoleUtils.Interfaces;

/// <summary>
/// Интерфейс для вывода данных пользователю.
/// </summary>
public interface IOutputService
{
    void WriteLine(string message, ConsoleColor color);
    void Write(string message, ConsoleColor color);
    void WriteError(string errorMessage);
    void OutputCatalog(string[] catalog);
}