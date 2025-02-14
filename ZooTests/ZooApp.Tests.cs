using Domain.Entities;
using Domain.Services;

namespace ZooTests;

using System;
using System.Linq;
using Xunit;


public class ZooTests
{
    // Тесты на валидацию при создании животных

    [Fact]
    public void CreatingHerboAnimal_WithGoodnessBelowRange_ThrowsArgumentOutOfRangeException()
    {
        // Попытка создать обезьяну с добротой 0 (допустимый диапазон: 1-10)
        Assert.Throws<ArgumentOutOfRangeException>(() => new Monkey(5, 0));
    }

    [Fact]
    public void CreatingHerboAnimal_WithGoodnessAboveRange_ThrowsArgumentOutOfRangeException()
    {
        // Попытка создать кролика с добротой 11 (допустимый диапазон: 1-10)
        Assert.Throws<ArgumentOutOfRangeException>(() => new Monkey(5, 11));
    }

    [Fact]
    public void CreatingAnimal_WithNegativeFood_ThrowsArgumentOutOfRangeException()
    {
        // Попытка создать тигра с отрицательной едой
        Assert.Throws<ArgumentOutOfRangeException>(() => new Tiger(-5));
    }

    [Fact]
    public void CreatingValidHerboAnimal_DoesNotThrowException()
    {
        // Создаём обезьяну с корректными параметрами
        var monkey = new Monkey(5, 7);
        Assert.Equal(5, monkey.Food);
        Assert.Equal(7, monkey.Goodness);
    }

    // Тесты для функциональности Zoo

    [Fact]
    public void AddAnimal_ValidAnimal_AddsAnimalToZoo()
    {
        var clinic = new VeterinaryClinic();
        var numberGenerator = new NumberGenerator();
        var zoo = new Domain.Services.Zoo(clinic, numberGenerator);

        var monkey = new Monkey(5, 7);
        bool accepted = zoo.AddAnimal(monkey);

        Assert.True(accepted);
        Assert.NotEqual(Guid.Empty, monkey.Number);
        Assert.Equal(1, zoo.GetAnimalsCount());
    }

    [Fact]
    public void GetTotalFoodPerDay_ReturnsCorrectSum()
    {
        var clinic = new VeterinaryClinic();
        var numberGenerator = new NumberGenerator();
        var zoo = new Domain.Services.Zoo(clinic, numberGenerator);

        zoo.AddAnimal(new Monkey(5, 7));   // 5 кг
        zoo.AddAnimal(new Tiger(10));      // 10 кг

        // Иногда тест не проходит, так животное может не добавиться из-за медкомиссии
        Assert.Equal(15, zoo.GetTotalFoodPerDay());
    }

    [Fact]
    public void GetAllInventoryItems_ReturnsBothAnimalsAndThings()
    {
        var clinic = new VeterinaryClinic();
        var numberGenerator = new NumberGenerator();
        var zoo = new Domain.Services.Zoo(clinic, numberGenerator);

        zoo.AddAnimal(new Monkey(5, 7));
        zoo.AddThing(new Table());

        var items = zoo.GetAllInventoryItems().ToList();
        Assert.Equal(2, items.Count);
    }
}
