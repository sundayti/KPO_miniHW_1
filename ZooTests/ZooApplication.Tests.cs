using ConsoleUtils;
using ConsoleUtils.Interfaces;
using Domain.Entities;
using Domain.Services;
using Zoo;

namespace ZooTests;

using System;
using Moq;
using Xunit;


// Testable-обёртка для ZooApplication, которая переопределяет метод Pause,
// чтобы тесты не зависали на ожидании ввода.
public class TestableZooApplication : ZooApplication
{
    public TestableZooApplication(Domain.Services.Zoo zoo,
                                  IMenuService menuService,
                                  IInputService inputService,
                                  IOutputService outputService)
        : base(zoo, menuService, inputService, outputService)
    {
    }

    protected void Pause() { }
}

public class ZooApplicationTests
{
    [Fact]
    public void Run_ExitImmediately_DoesNotThrow()
    {
        // Arrange
        var zoo = new Domain.Services.Zoo(new VeterinaryClinic(), new NumberGenerator());

        var menuServiceMock = new Mock<IMenuService>();
        // Сразу выбираем опцию "6" (выход)
        menuServiceMock.Setup(m => m.GetMainMenuSelection()).Returns(6);
        menuServiceMock.Setup(m => m.DisplayMainMenu());

        var inputServiceMock = new Mock<IInputService>();
        var outputServiceMock = new Mock<IOutputService>();

        var app = new TestableZooApplication(zoo, menuServiceMock.Object, inputServiceMock.Object, outputServiceMock.Object);

        // Act & Assert
        var exception = Record.Exception(() => app.Run());
        Assert.Null(exception);
    }
}
