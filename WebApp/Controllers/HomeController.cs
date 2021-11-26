using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFac;
        private readonly IConfiguration _config;
        private readonly string apiUrl, grpcUrl;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFac, IConfiguration config)
        {
            _logger = logger;
            _httpClientFac = httpClientFac;
            _config = config;
            this.apiUrl = _config.GetValue<string>("BenimApiUrl");
            this.grpcUrl = _config.GetValue<string>("BenimApiGrpcUrl");
        }

        public async Task<IActionResult> IndexAsync()
        {
            using var c = _httpClientFac.CreateClient();
            var apiTask = c.GetStreamAsync(apiUrl + "/weatherforecasts");
            var weather = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(await apiTask);
            
            return View(weather);
        }

        public async Task<IActionResult> GrpcAsync()
        {
            var httpHandler = new HttpClientHandler();

#if false
            // todo : could not get trusted internal certs in docker-compose.
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
#endif

            using var channel = GrpcChannel.ForAddress(grpcUrl, new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new BenimApi.Grpc.WeatherService.WeatherServiceClient(channel);

            var result = await client.GetForecastAsync(new BenimApi.Grpc.ForecastRequest());

            return View(result.Results.ToList());
        }


        public IActionResult Privacy()
        {


            return View();
        }

        public async Task<IActionResult> SlowViewAsync()
        {
            var k = "BatMan";
            for (int i = 0; i < 10000; i++)
            {
                k += "NaN" + i.ToString();
            }
            k.ToUpper().Trim();

            using var c = _httpClientFac.CreateClient();
            var apiTask = c.GetStreamAsync(apiUrl + "/weatherforecasts");
            
            var weather = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(await apiTask);

            return View(weather);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}
