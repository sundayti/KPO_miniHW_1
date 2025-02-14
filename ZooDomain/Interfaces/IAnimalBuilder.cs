using Domain.Entities;

namespace Domain.Builders;

/// <summary>
/// Интерфейс для строителей животных (Builder)
/// </summary>
public interface IAnimalBuilder
{
    /// <summary>   
    /// Установить суточную норму еды
    /// </summary>
    IAnimalBuilder SetFood(int food);

    /// <summary>
    /// Установить уровень "доброты" (для травоядных)
    /// </summary>
    IAnimalBuilder SetGoodness(int goodness);

    /// <summary>
    /// Вернуть готовый объект животного
    /// </summary>
    Animal Build();
}