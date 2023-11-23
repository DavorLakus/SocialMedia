using System.Text.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Social.Models;

namespace Social.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IApiService _apiService;

    public HomeController(ILogger<HomeController> logger, IApiService apiService)
    {
        _logger = logger;
        _apiService = apiService;
    }
    public IActionResult Index()
    {
        return View();
    }

   public async Task<IActionResult> Privacy()
    {
        // Calculate Unix timestamps for two weeks ago and now
        var twoWeeksAgoTimestamp = DateTimeOffset.UtcNow.AddDays(-14).ToUnixTimeSeconds();
        var nowTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Fetch mentions for two weeks ago, now, and 100
        var mentions = await _apiService.GetV1Mentions(twoWeeksAgoTimestamp, nowTimestamp, 100);

        // Pass the data as a model to the Privacy view
        return View(mentions);
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
