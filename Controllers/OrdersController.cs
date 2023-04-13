using Microsoft.AspNetCore.Mvc;
using MemoryCacheWebAPI_Demo.Models;
using MemoryCacheWebAPI_Demo.Services;

namespace MemoryCacheWebAPI_Demo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderService _orderService;

    public OrdersController(ILogger<OrdersController> logger, IOrderService orderService)
    {
        _logger = logger;
        _orderService = orderService;
    }

    public async Task<IActionResult> GetOrders()
    {
        List<Order> orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost]
    public IActionResult ClearCache()
    {
        _orderService.ClearCache();
        return Ok();
    }

}

