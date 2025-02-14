using Domain.Builders;
using Domain.Directors;
using Domain.Entities;

namespace ZooTests;

public class BuilderDirectorTests
    {
        [Fact]
        public void MonkeyBuilder_ConstructHerboAnimal_ReturnsMonkeyWithCorrectValues()
        {
            // Arrange
            int food = 10;
            int goodness = 8;
            IAnimalBuilder builder = new MonkeyBuilder();
            var director = new AnimalDirector(builder);

            // Act
            Animal animal = director.ConstructHerboAnimal(food, goodness);

            // Assert
            Assert.IsType<Monkey>(animal);
            var monkey = animal as Monkey;
            Assert.NotNull(monkey);
            Assert.Equal(food, monkey.Food);
            Assert.Equal(goodness, monkey.Goodness);
        }

        [Fact]
        public void RabbitBuilder_ConstructHerboAnimal_ReturnsRabbitWithCorrectValues()
        {
            // Arrange
            int food = 12;
            int goodness = 6;
            IAnimalBuilder builder = new RabbitBuilder();
            var director = new AnimalDirector(builder);

            // Act
            Animal animal = director.ConstructHerboAnimal(food, goodness);

            // Assert
            Assert.IsType<Rabbit>(animal);
            var rabbit = animal as Rabbit;
            Assert.NotNull(rabbit);
            Assert.Equal(food, rabbit.Food);
            Assert.Equal(goodness, rabbit.Goodness);
        }

        [Fact]
        public void TigerBuilder_ConstructPredatorAnimal_ReturnsTigerWithCorrectFood()
        {
            // Arrange
            int food = 15;
            IAnimalBuilder builder = new TigerBuilder();
            var director = new AnimalDirector(builder);

            // Act
            Animal animal = director.ConstructPredatorAnimal(food);

            // Assert
            Assert.IsType<Tiger>(animal);
            var tiger = animal as Tiger;
            Assert.NotNull(tiger);
            Assert.Equal(food, tiger.Food);
        }

        [Fact]
        public void WolfBuilder_ConstructPredatorAnimal_ReturnsWolfWithCorrectFood()
        {
            // Arrange
            int food = 9;
            IAnimalBuilder builder = new WolfBuilder();
            var director = new AnimalDirector(builder);

            // Act
            Animal animal = director.ConstructPredatorAnimal(food);

            // Assert
            Assert.IsType<Wolf>(animal);
            var wolf = animal as Wolf;
            Assert.NotNull(wolf);
            Assert.Equal(food, wolf.Food);
        }

        [Fact]
        public void Builder_Chaining_ReturnsSameInstance()
        {
            // Arrange
            IAnimalBuilder builder = new MonkeyBuilder();
            
            // Act
            var builderAfterFood = builder.SetFood(10);
            var builderAfterGoodness = builderAfterFood.SetGoodness(8);
            
            // Assert: проверяем, что методы возвращают тот же экземпляр (для возможности цепочки вызовов)
            Assert.Same(builder, builderAfterFood);
            Assert.Same(builder, builderAfterGoodness);
        }
    }