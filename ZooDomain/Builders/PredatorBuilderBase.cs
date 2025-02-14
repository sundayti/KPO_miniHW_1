using Domain.Entities;

namespace Domain.Builders;

public abstract class PredatorBuilderBase : IAnimalBuilder
{
    protected int Food;

    public IAnimalBuilder SetGoodness(int goodness) => this;

    public IAnimalBuilder SetFood(int food)
    {
        Food = food;
        return this;
    }

    public abstract Animal Build();
}