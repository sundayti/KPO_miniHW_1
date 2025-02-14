using Domain.Entities;

namespace Domain.Builders;

public class RabbitBuilder : HerboBuilderBase
{
    public override Animal Build()
    {
        return new Rabbit(Food, Goodness);
    }
}