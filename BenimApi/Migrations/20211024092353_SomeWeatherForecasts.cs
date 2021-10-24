using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BenimApi.Migrations
{
    public partial class SomeWeatherForecasts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Id", "Date", "TemperatureC", "Summary" },
                values: new object[] {"1", DateTime.Now , 27 , "Sunny" }
            );


            migrationBuilder.InsertData(
                table: "WeatherForecast",
                columns: new[] { "Id", "Date", "TemperatureC", "Summary" },
                values: new object[] { "2", DateTime.Now, -4, "Ice cold" }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
