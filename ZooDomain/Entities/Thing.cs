using Domain.Interfaces;

namespace Domain.Entities;

public abstract class Thing : IInventory
{
    public Guid Number { get; set; } = Guid.Empty;

    public override string ToString()
    {
        return $"{GetType().Name} (#{Number})";
    }
}