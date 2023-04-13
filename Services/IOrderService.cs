using MemoryCacheWebAPI_Demo.Models;

namespace MemoryCacheWebAPI_Demo.Services;

public interface IOrderService
{
    Task<List<Order>> GetAllAsync();

    void ClearCache();
}
