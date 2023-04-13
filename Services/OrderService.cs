using MemoryCacheWebAPI_Demo.Controllers;
using MemoryCacheWebAPI_Demo.Data;
using MemoryCacheWebAPI_Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace MemoryCacheWebAPI_Demo.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrdersController> _logger;

    public OrderService(ILogger<OrdersController> logger, ApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public virtual void ClearCache()
    {
    }

    public async Task<List<Order>> GetAllAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }
}