using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Services;

public class Zoo(
    IVeterinaryClinic veterinaryClinic,
    IInventoryNumberGenerator numberGenerator)
{
    private readonly List<IInventory> _inventory = new();
    
    public bool AddAnimal(Animal animal)
    {
        if (!veterinaryClinic.CheckAnimal(animal)) return false;
        animal.Number = numberGenerator.GetNextNumber();
        _inventory.Add(animal);
        return true;
    }
    
    public void AddThing(Thing thing)
    {
        thing.Number = numberGenerator.GetNextNumber();
        _inventory.Add(thing);
    }
    
    public int GetAnimalsCount() => _inventory.OfType<Animal>().Count();
    
    public int GetTotalFoodPerDay() =>
        _inventory.OfType<IAlive>().Sum(a => a.Food);
    
    public IEnumerable<Herbo> GetContactZooAnimals() =>
        _inventory.OfType<Herbo>().Where(x => x.CanBeContactAnimal);
    
    public IEnumerable<IInventory> GetAllInventoryItems() => _inventory;
}
