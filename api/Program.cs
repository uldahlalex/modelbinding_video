using Microsoft.EntityFrameworkCore;
using service;
using videoprep;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HospitalContext>(options =>
    {
        options.UseSqlite(("DataSource=../dataaccess/db.db"));
        options.EnableSensitiveDataLogging();
    }
  );
builder.Services.AddScoped<IGenericService, GenericService>();
builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers();

app.Run();
