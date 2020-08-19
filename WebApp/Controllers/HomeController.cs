using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFac;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFac)
        {
            _logger = logger;
            _httpClientFac = httpClientFac;
        }

        public async Task<IActionResult> IndexAsync()
        {
            using var c = _httpClientFac.CreateClient();
            var apiTask = c.GetStreamAsync("https://localhost:44386/weatherforecasts");
            var weather = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(await apiTask);


            return View(weather);
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

   
}
