using System.Threading; 
namespace Domain.Services;

public class NumberGenerator : IInventoryNumberGenerator
{
    public Guid GetNextNumber()
    {
        return Guid.NewGuid();
    }
}
