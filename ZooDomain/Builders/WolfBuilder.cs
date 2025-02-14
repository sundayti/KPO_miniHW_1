using Domain.Entities;

namespace Domain.Builders;

public class WolfBuilder : PredatorBuilderBase
{
    public override Animal Build()
    {
        return new Wolf(Food);
    }
}