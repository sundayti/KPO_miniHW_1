using Domain.Builders;
using Domain.Entities;

namespace Domain.Directors;

public class AnimalDirector(IAnimalBuilder builder)
{
    public Animal ConstructHerboAnimal(int food, int goodness)
    {
        return builder
            .SetFood(food)
            .SetGoodness(goodness)
            .Build();
    }

    public Animal ConstructPredatorAnimal(int food)
    {
        return builder
            .SetFood(food)
            .Build();
    }
}
