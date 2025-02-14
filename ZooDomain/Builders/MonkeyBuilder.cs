using Domain.Entities;

namespace Domain.Builders;

public class MonkeyBuilder : HerboBuilderBase
{
    public override Animal Build()
    {
        return new Monkey(Food, Goodness);
    }
}