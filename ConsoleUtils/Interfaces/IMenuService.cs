namespace ConsoleUtils;

/// <summary>
/// Интерфейс для сервиса меню.
/// </summary>
public interface IMenuService
{
    void DisplayMainMenu();
    int GetMainMenuSelection();
}