using ConsoleUtils.Interfaces;

namespace ConsoleUtils;

/// <summary>
/// Сервис для отображения главного меню и получения выбора пользователя.
/// </summary>
public class MenuService : IMenuService
{
    private readonly IOutputService _output;
    private readonly IInputService _input;

    public MenuService(IOutputService output, IInputService input)
    {
        _output = output;
        _input = input;
    }

    public void DisplayMainMenu()
    {
        Console.Clear();
        _output.WriteLine("Выберите действие:", ConsoleColor.Yellow);

        string[] actions =
        {
            "Добавить новое животное",
            "Показать общее количество животных",
            "Показать суммарную потребность в еде (кг/сутки)",
            "Показать животных, подходящих для контактного зоопарка",
            "Показать список всех инвентарных объектов",
            "Завершить программу"
        };

        _output.OutputCatalog(actions);
    }

    public int GetMainMenuSelection()
    {
        _output.WriteLine("Нажмите соответствующую цифру для выбора опции:", ConsoleColor.White);
        var key = _input.ReadKey();
        Console.WriteLine(); // переход на новую строку после нажатия
        // Сопоставляем нажатую клавишу с номером пункта меню:
        if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1) return 1;
        if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2) return 2;
        if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3) return 3;
        if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4) return 4;
        if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5) return 5;
        if (key.Key == ConsoleKey.D6 || key.Key == ConsoleKey.NumPad6) return 6;
        return 0; // если не распознано
    }
}