using MemoryCacheWebAPI_Demo.Controllers;
using MemoryCacheWebAPI_Demo.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MemoryCacheWebAPI_Demo.Services;

public class CachedOrderService : IOrderService
{
    private readonly IOrderService _orderService;
    private readonly IMemoryCache _cache;
    private readonly ILogger<OrdersController> _logger;
    private readonly string _cacheKey = "ordersCacheKey";

    public CachedOrderService(ILogger<OrdersController> logger, IOrderService orderService, IMemoryCache cache)
    {
        _logger = logger;
        _orderService = orderService;
        _cache = cache;
    }

    public void ClearCache()
    {
        _cache.Remove(_cacheKey);
        _logger.LogInformation("Cache was cleared");
    }

    public async Task<List<Order>> GetAllAsync()
    {
        if (_cache.TryGetValue(_cacheKey, out List<Order>? orders))
        {
            _logger.LogInformation("Orders found in cache");
        }
        else
        {
            _logger.LogInformation("Orders not found in cache, try get it from DB");

            orders = await _orderService.GetAllAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.Normal);

            _cache.Set(_cacheKey, orders, cacheEntryOptions);
        }

        if (orders == null)
        {
            return new List<Order>();
        }

        return orders.ToList();
    }
}