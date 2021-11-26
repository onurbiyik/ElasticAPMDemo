using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class WeatherForecast
    {
        public DateTime date { get; set; }

        public int Temperaturec { get; set; }

        public int temperaturef => 32 + (int)(Temperaturec / 0.5556);

        public string? summary { get; set; }
    }
}
