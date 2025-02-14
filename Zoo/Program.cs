using ConsoleUtils;
using ConsoleUtils.Interfaces;
using Domain.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Zoo;

public static class Program
{
    public static void Main(string[] args)
    {
        var services = new ServiceCollection();
        
        // Регистрация доменных сервисов
        services.AddSingleton<IVeterinaryClinic, VeterinaryClinic>();
        services.AddSingleton<IInventoryNumberGenerator, NumberGenerator>();
        services.AddSingleton<Domain.Services.Zoo>();

        // Регистрация UI-сервисов
        services.AddSingleton<IInputService, ConsoleInputService>();
        services.AddSingleton<IOutputService, ConsoleOutputService>();
        services.AddSingleton<IMenuService, MenuService>();

        // Регистрация приложения
        services.AddSingleton<ZooApplication>();

        var serviceProvider = services.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<ZooApplication>();
        app.Run();
    }
}