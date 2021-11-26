using BenimApi.Data;
using BenimApi.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BenimApi.Services;

public class WeatherService : Grpc.WeatherService.WeatherServiceBase
{
    private readonly BenimDbContext _context;

    public WeatherService(BenimDbContext context)
    {
        _context = context;
    }

    public override async Task<ForecastReply> GetForecast(ForecastRequest request, ServerCallContext context)
    {

        var rows = await _context.WeatherForecast.ToListAsync();

        
        var reply =  new ForecastReply();

        foreach (var row in rows)
        {
            reply.Results.Add(new Forecast() { 
                Id = row.Id,
                Summary = row.Summary,
                TemperatureC = row.TemperatureC,
                Date = Timestamp.FromDateTime( row.Date.ToUniversalTime())
            });

        }

        return reply;

    }
}
