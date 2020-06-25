using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BenimApi;

namespace BenimApi.Data
{
    public class BenimDbContext : DbContext
    {
        public BenimDbContext (DbContextOptions<BenimDbContext> options)
            : base(options)
        {
        }

        public DbSet<BenimApi.WeatherForecast> WeatherForecast { get; set; }
    }
}
