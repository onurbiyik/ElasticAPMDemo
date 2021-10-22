using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenimApi.Data;
using Microsoft.Data.SqlClient;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace BenimApi
{
    [Route("[controller]")]
    [ApiController]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly BenimDbContext _context;
        private readonly IHttpClientFactory _httpClientFac;
        private readonly IConfiguration _config;

        public WeatherForecastsController(BenimDbContext context, IHttpClientFactory httpClientFac, IConfiguration config)
        {
            _context = context;
            _httpClientFac = httpClientFac;
            _config = config;
        }

        // GET: api/WeatherForecasts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetWeatherForecast()
        {
            var rnd = new Random().NextDouble();
            if (rnd < 0.08)
                throw new ApplicationException("uygulama patladı.");

            using var c = _httpClientFac.CreateClient();

            var tasks = new List<Task>();
            if (rnd < 0.15)
            {
                
                tasks.Add( c.GetAsync("http://www.brazilmotorsandcontrols.com/Home.html"));
                tasks.Add(c.GetAsync("http://www.veoliawatertech.com/latam/es/"));

            }

            if (rnd > 0.05 && rnd < 0.25)
            {
                System.Threading.Thread.Sleep(Convert.ToInt32(10000 * (rnd - 0.02)));
            }

            if (rnd > 0.9)
            {                
                tasks.Add(c.GetStringAsync("https://www.ferrari.com/fr-FR"));
                var sahib = c.GetAsync("https://www.ferrari.com/en-US").ContinueWith(async (task) => { await task.Result.Content.ReadAsStringAsync(); });
                tasks.Add(sahib);
            }

            Task.WaitAll(tasks.ToArray());


            return await _context.WeatherForecast.ToListAsync();
        }

        // GET: api/WeatherForecasts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherForecast>> GetWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecast.FindAsync(id);

            if (weatherForecast == null)
            {
                return NotFound();
            }

            return weatherForecast;
        }

        // PUT: api/WeatherForecasts/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherForecast(int id, WeatherForecast weatherForecast)
        {
            if (id != weatherForecast.Id)
            {
                return BadRequest();
            }

            _context.Entry(weatherForecast).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherForecastExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/WeatherForecasts
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WeatherForecast>> PostWeatherForecast(WeatherForecast weatherForecast)
        {
            _context.WeatherForecast.Add(weatherForecast);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeatherForecast", new { id = weatherForecast.Id }, weatherForecast);
        }

        // DELETE: api/WeatherForecasts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WeatherForecast>> DeleteWeatherForecast(int id)
        {
            var weatherForecast = await _context.WeatherForecast.FindAsync(id);
            if (weatherForecast == null)
            {
                return NotFound();
            }

            _context.WeatherForecast.Remove(weatherForecast);
            await _context.SaveChangesAsync();

            return weatherForecast;
        }

        private bool WeatherForecastExists(int id)
        {
            return _context.WeatherForecast.Any(e => e.Id == id);
        }
    }
}
