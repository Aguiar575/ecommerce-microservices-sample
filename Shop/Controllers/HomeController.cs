using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SnowflakeId()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7200/");
            
            var responseTask = client.PostAsync("snowflake-id", null);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                SnowflakeIdViewModel snowflakeId = 
                    result.Content.ReadFromJsonAsync<SnowflakeIdViewModel>().Result;
                ViewBag.SnowflakeId = snowflakeId;
            }
            else
                ViewBag.SnowflakeId = new SnowflakeIdViewModel(123);
        }
        
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
