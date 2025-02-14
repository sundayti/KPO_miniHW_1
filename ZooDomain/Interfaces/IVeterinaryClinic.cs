using Domain.Entities;

namespace Domain.Services;

public interface IVeterinaryClinic
{
    /// <summary>
    /// Проверить здоровье животного
    /// true – животное здорово и может быть принято в зоопарк
    /// false – животное не проходит проверку
    /// </summary>
    bool CheckAnimal(Animal animal);
}