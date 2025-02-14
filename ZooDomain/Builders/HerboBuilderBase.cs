using Domain.Entities;

namespace Domain.Builders;

/// <summary>
/// Базовый абстрактный класс для строителей травоядных (Herbo)
/// </summary>
public abstract class HerboBuilderBase : IAnimalBuilder
{
    protected int Food;
    protected int Goodness;

    public IAnimalBuilder SetFood(int food)
    {
        Food = food;
        return this;
    }

    public IAnimalBuilder SetGoodness(int goodness)
    {
        Goodness = goodness;
        return this;
    }

    public abstract Animal Build();
}