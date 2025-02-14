using Domain.Interfaces;

namespace Domain.Entities;

public abstract class Animal : IAlive, IInventory
{

    private int _food;

    public int Food
    {
        get => _food;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(Food), "Food must be greater than 0.");
            _food = value;
        }
    }
    
    protected Animal(int food)
    {
        if (food <= 0)
            throw new ArgumentOutOfRangeException(nameof(food), "Food must be greater than 0.");
        _food = food;
        Number = Guid.Empty;
    }
    
    public Guid Number { get; set; } = Guid.Empty;

    public override string ToString()
    {
        return $"{GetType().Name} (#{Number}): потребляет {Food} кг/сутки";
    }
}