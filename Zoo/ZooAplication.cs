using ConsoleUtils;
using ConsoleUtils.Interfaces;
using Domain.Builders;
using Domain.Directors;
using Domain.Entities;
using Domain.Services;

namespace Zoo;

public class ZooApplication(
    Domain.Services.Zoo zoo,
    IMenuService menuService,
    IInputService inputService,
    IOutputService outputService)
{
    public void Run()
    {
        // Для демонстрации автоматически добавляем несколько предметов
        zoo.AddThing(new Table());
        zoo.AddThing(new Computer());

        bool exit = false;
        while (!exit)
        {
            DisplayMainMenu();
            int selection = menuService.GetMainMenuSelection();

            switch (selection)
            {
                case 1:
                    AddAnimal();
                    break;
                case 2:
                    outputService.WriteLine($"Всего животных: {zoo.GetAnimalsCount()}", ConsoleColor.Green);
                    Pause();
                    break;
                case 3:
                    outputService.WriteLine($"Суммарное потребление еды: {zoo.GetTotalFoodPerDay()} кг/сутки", ConsoleColor.Green);
                    Pause();
                    break;
                case 4:
                    DisplayContactZooAnimals();
                    Pause();
                    break;
                case 5:
                    DisplayInventory();
                    Pause();
                    break;
                case 6:
                    AddThing();
                    break;
                case 7:
                    exit = true;
                    break;
                default:
                    outputService.WriteError("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }

    private void DisplayMainMenu()
    {
        Console.Clear();
        outputService.WriteLine("Выберите действие:", ConsoleColor.Yellow);
        string[] actions =
        {
            "Добавить новое животное",
            "Показать общее количество животных",
            "Показать суммарное потребление еды (кг/сутки)",
            "Показать животных, подходящих для контактного зоопарка",
            "Показать список всех инвентарных объектов",
            "Добавить предмет",
            "Завершить программу"
        };
        outputService.OutputCatalog(actions);
    }

    private void AddAnimal()
    {
        Console.Clear();
        outputService.WriteLine("Выберите тип животного для добавления:", ConsoleColor.Yellow);
        string[] types = { "Обезьяна", "Кролик", "Тигр", "Волк" };
        outputService.OutputCatalog(types);
        outputService.WriteLine("Нажмите соответствующую цифру для выбора:", ConsoleColor.White);

        var key = inputService.ReadKey();
        Console.WriteLine();

        Animal newAnimal = null;
        switch (key.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                {
                    outputService.WriteLine("Введите суточную норму еды (кг):", ConsoleColor.White);
                    int food = inputService.ReadInt();
                    outputService.WriteLine("Введите уровень доброты (1..10):", ConsoleColor.White);
                    int goodness = inputService.ReadInt(10);
                    
                    IAnimalBuilder builder = new MonkeyBuilder();
                    var director = new AnimalDirector(builder);
                    newAnimal = director.ConstructHerboAnimal(food, goodness);
                    break;
                }
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                {
                    outputService.WriteLine("Введите суточную норму еды (кг):", ConsoleColor.White);
                    int food = inputService.ReadInt();
                    outputService.WriteLine("Введите уровень доброты (1..10):", ConsoleColor.White);
                    int goodness = inputService.ReadInt(10);
                    
                    IAnimalBuilder builder = new RabbitBuilder();
                    var director = new AnimalDirector(builder);
                    newAnimal = director.ConstructHerboAnimal(food, goodness);
                    break;
                }
            case ConsoleKey.D3:
            case ConsoleKey.NumPad3:
                {
                    outputService.WriteLine("Введите суточную норму еды (кг):", ConsoleColor.White);
                    int food = inputService.ReadInt();
                    
                    IAnimalBuilder builder = new TigerBuilder();
                    var director = new AnimalDirector(builder);
                    newAnimal = director.ConstructPredatorAnimal(food);
                    break;
                }
            case ConsoleKey.D4:
            case ConsoleKey.NumPad4:
                {
                    outputService.WriteLine("Введите суточную норму еды (кг):", ConsoleColor.White);
                    int food = inputService.ReadInt();
                    
                    IAnimalBuilder builder = new WolfBuilder();
                    var director = new AnimalDirector(builder);
                    newAnimal = director.ConstructPredatorAnimal(food);
                    break;
                }
            default:
                outputService.WriteError("Неверный выбор типа животного.");
                return;
        }

        if (newAnimal != null)
        {
            bool accepted = zoo.AddAnimal(newAnimal);
            if (accepted)
            {
                outputService.WriteLine("Животное успешно добавлено!", ConsoleColor.Green);
            }
            else
            {
                outputService.WriteError("Животное отклонено ветеринарной клиникой.");
            }
        }
        Pause();
    }

    private void AddThing()
    {
        Console.Clear();
        outputService.WriteLine("Выберите тип предмета для добавления:", ConsoleColor.Yellow);
        string[] types = { "Стул", "Компьютер" };
        outputService.OutputCatalog(types);
        outputService.WriteLine("Нажмите соответствующую цифру для выбора:", ConsoleColor.White);

        var key = inputService.ReadKey();
        Console.WriteLine();

        Thing newThing = null;
        switch (key.Key)
        {
            case ConsoleKey.D1:
            case ConsoleKey.NumPad1:
                {
                    newThing = new Table();
                    break;
                }
            case ConsoleKey.D2:
            case ConsoleKey.NumPad2:
                {
                    newThing = new Computer();
                    break;
                }
            default:
                outputService.WriteError("Неверный выбор типа предмета.");
                return;
        }

        if (newThing != null)
        {
            zoo.AddThing(newThing);
            outputService.WriteLine("Предмет успешно добавлен!", ConsoleColor.Green);
        }
        Pause();
    }

    private void DisplayContactZooAnimals()
    {
        outputService.WriteLine("Животные для контактного зоопарка:", ConsoleColor.Yellow);
        foreach (var animal in zoo.GetContactZooAnimals())
        {
            outputService.WriteLine(animal.ToString(), ConsoleColor.Cyan);
        }
    }

    private void DisplayInventory()
    {
        outputService.WriteLine("Все инвентарные объекты:", ConsoleColor.Yellow);
        foreach (var item in zoo.GetAllInventoryItems())
        {
            outputService.WriteLine(item.ToString(), ConsoleColor.Cyan);
        }
    }

    private void Pause()
    {
        Console.WriteLine("\nНажмите любую клавишу для продолжения...");
        Console.ReadKey();
    }
}