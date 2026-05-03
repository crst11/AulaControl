using AulaControl.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 4, 0))
    );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();