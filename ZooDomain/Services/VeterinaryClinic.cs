using Domain.Entities;

namespace Domain.Services;

public class VeterinaryClinic : IVeterinaryClinic
{
    public bool CheckAnimal(Animal animal)
    {
        var rnd = new Random();
        return rnd.NextDouble() < 0.9;
    }
}