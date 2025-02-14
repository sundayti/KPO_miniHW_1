namespace Domain.Entities;

/// <summary>
/// Травоядное животное
/// </summary>
public abstract class Herbo : Animal
{
    private int _goodness;
    
    public int Goodness
    {
        get => _goodness;
        set
        {
            if (value <= 0 || value > 10)
                throw new ArgumentOutOfRangeException(nameof(Goodness), "Goodness must be between 0 and 10.");
            _goodness = value;
        }
    }

    protected Herbo(int food, int goodness) : base(food)
    {
        Goodness = goodness;
    }

    public bool CanBeContactAnimal => Goodness > 5;

    public override string ToString()
    {
        return base.ToString() + $", доброта: {Goodness}";
    }
}