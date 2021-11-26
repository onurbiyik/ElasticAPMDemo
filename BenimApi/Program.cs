using BenimApi.Data;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpClient();
builder.Services.AddControllers();

//builder.Services.AddDbContext<BenimDbContext>(opts =>
//{
//    var connString = builder.Configuration.GetConnectionString("BenimDbContext");
//    opts.UseSqlServer(connString, options =>
//    {
//        options.MigrationsAssembly(typeof(BenimDbContext).Assembly.FullName.Split(',')[0]);
//    });
//});

builder.Services.AddDbContext<BenimDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("BenimDbContext")));


var app = builder.Build();

using (var dbContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<BenimDbContext>())
{
    dbContext.Database.Migrate();
}

app.UseAllElasticApm(builder.Configuration);

// app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
