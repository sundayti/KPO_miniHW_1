using ConsoleUtils;
using ConsoleUtils.Interfaces;
using Moq;

namespace ZooTests;

public class MenuServiceTests
    {
        [Theory]
        [InlineData(ConsoleKey.D1, 1)]
        [InlineData(ConsoleKey.NumPad1, 1)]
        [InlineData(ConsoleKey.D2, 2)]
        [InlineData(ConsoleKey.NumPad2, 2)]
        [InlineData(ConsoleKey.D3, 3)]
        [InlineData(ConsoleKey.NumPad3, 3)]
        [InlineData(ConsoleKey.D4, 4)]
        [InlineData(ConsoleKey.NumPad4, 4)]
        [InlineData(ConsoleKey.D5, 5)]
        [InlineData(ConsoleKey.NumPad5, 5)]
        [InlineData(ConsoleKey.D6, 6)]
        [InlineData(ConsoleKey.NumPad6, 6)]
        [InlineData(ConsoleKey.A, 0)] // для любой другой клавиши
        public void GetMainMenuSelection_Returns_CorrectOption(ConsoleKey key, int expected)
        {
            // Arrange: создаём моки для IOutputService и IInputService
            var outputMock = new Mock<IOutputService>();
            var inputMock = new Mock<IInputService>();
            // Настраиваем ReadKey() так, чтобы возвращалась нужная клавиша
            inputMock.Setup(i => i.ReadKey())
                     .Returns(new ConsoleKeyInfo('x', key, false, false, false));

            var menuService = new MenuService(outputMock.Object, inputMock.Object);

            // Act
            int result = menuService.GetMainMenuSelection();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DisplayMainMenu_Calls_OutputCatalog_And_WriteLine()
        {
            // Arrange
            var outputMock = new Mock<IOutputService>();
            var inputMock = new Mock<IInputService>();

            var menuService = new MenuService(outputMock.Object, inputMock.Object);

            // Act
            menuService.DisplayMainMenu();

            // Assert:
            // Проверяем, что WriteLine вызывается с заголовком меню
            outputMock.Verify(o => o.WriteLine("Выберите действие:", ConsoleColor.Yellow), Times.Once);
            // Проверяем, что OutputCatalog вызывается с массивом из 6 пунктов
            outputMock.Verify(o => o.OutputCatalog(It.Is<string[]>(arr => arr.Length == 6)), Times.Once);
        }
    }

    public class ConsoleOutputServiceTests
    {
        [Fact]
        public void WriteLine_WritesMessageToConsole()
        {
            // Arrange: перенаправляем стандартный вывод в StringWriter
            var originalOut = Console.Out;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                var outputService = new ConsoleOutputService();

                // Act
                outputService.WriteLine("Test Message", ConsoleColor.Green);

                // Reset стандартного вывода
                Console.SetOut(originalOut);
                string result = writer.ToString();

                // Assert: проверяем, что вывод содержит "Test Message"
                Assert.Contains("Test Message", result);
            }
        }

        [Fact]
        public void OutputCatalog_WritesCatalogWithIndices()
        {
            // Arrange
            var originalOut = Console.Out;
            using (var writer = new StringWriter())
            {
                Console.SetOut(writer);
                var outputService = new ConsoleOutputService();
                string[] catalog = { "Item1", "Item2" };

                // Act
                outputService.OutputCatalog(catalog);

                // Reset
                Console.SetOut(originalOut);
                string result = writer.ToString();

                // Assert: проверяем наличие индексов и имен элементов
                Assert.Contains("1 -", result);
                Assert.Contains("Item1", result);
                Assert.Contains("2 -", result);
                Assert.Contains("Item2", result);
            }
        }
    }

    public class ConsoleInputServiceTests
    {
        [Fact]
        public void ReadInt_ValidInput_ReturnsParsedInteger()
        {
            // Arrange: подменяем стандартный ввод
            var originalIn = Console.In;
            using (var reader = new StringReader("42\n"))
            {
                Console.SetIn(reader);
                // Для ConsoleInputService передаём мок IOutputService, который здесь не важен
                var outputMock = new Mock<IOutputService>();
                var inputService = new ConsoleInputService(outputMock.Object);

                // Act
                int result = inputService.ReadInt();

                // Reset
                Console.SetIn(originalIn);

                // Assert
                Assert.Equal(42, result);
            }
        }

        [Fact]
        public void ReadDouble_ValidInput_ReturnsParsedDouble()
        {
            // Arrange
            var originalIn = Console.In;
            using (var reader = new StringReader("3.14\n"))
            {
                Console.SetIn(reader);
                var outputMock = new Mock<IOutputService>();
                var inputService = new ConsoleInputService(outputMock.Object);

                // Act
                double result = inputService.ReadDouble();

                // Reset
                Console.SetIn(originalIn);

                // Assert
                Assert.Equal(3.14, result, 2);
            }
        }

        [Fact]
        public void ReadInt_InvalidThenValidInput_WritesErrorMessage()
        {
            // Arrange: сначала передаём некорректный ввод, затем корректный
            var originalIn = Console.In;
            using (var reader = new StringReader("abc\n10\n"))
            {
                Console.SetIn(reader);
                var outputMock = new Mock<IOutputService>();
                var inputService = new ConsoleInputService(outputMock.Object);

                // Act
                int result = inputService.ReadInt();

                // Reset
                Console.SetIn(originalIn);

                // Assert
                Assert.Equal(10, result);
                // Проверяем, что при некорректном вводе выводится сообщение об ошибке
                outputMock.Verify(o => o.WriteLine("Invalid data! Please try again.", ConsoleColor.Red),
                                    Times.AtLeastOnce);
            }
        }
    }