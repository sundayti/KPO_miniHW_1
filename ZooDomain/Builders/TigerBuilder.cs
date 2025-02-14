using Domain.Entities;

namespace Domain.Builders;

public class TigerBuilder : PredatorBuilderBase
{
    public override Animal Build()
    {
        return new Tiger(Food);
    }
}