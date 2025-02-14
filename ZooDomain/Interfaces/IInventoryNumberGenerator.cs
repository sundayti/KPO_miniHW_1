namespace Domain.Services;


/// <summary>
/// Сервис для генерации уникальных инвентарных номеров
/// </summary>
public interface IInventoryNumberGenerator
{
    /// <summary>
    /// Возвращает очередной уникальный номер
    /// </summary>
    Guid GetNextNumber();
}