using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.Services;

namespace Shop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SnowflakeService _snowflakeService;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        _snowflakeService = new SnowflakeService();
    }

    public IActionResult Index() => View();

    public async Task<IActionResult> SnowflakeId()
    {
        ViewBag.SnowflakeId = await _snowflakeService.SnowflakeId();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
