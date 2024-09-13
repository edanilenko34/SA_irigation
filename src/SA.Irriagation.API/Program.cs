using Microsoft.EntityFrameworkCore;
using SA.Irrigation.API.Extensions;
using SA.Irrigation.Db;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConfigiration(builder.Configuration);
builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddCustomServices(builder.Configuration);



var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IrrigationDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
