using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlatformService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IPlatformRepo, PlatformRepo>();
builder.Services.AddDbContext<AppDbContext>(opt =>
                     opt.UseInMemoryDatabase("InMem"));
builder.Services.AddDbContext<AppDbContext>(opt =>
                   opt.UseSqlServer(builder.Configuration.GetConnectionString("PlatformsConn")));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

PrepDb.PrepPopulation(app, app.Environment.IsProduction());  //issue might come from this place

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
