using Microsoft.EntityFrameworkCore;
using Transactions.Api.Data;
using Transactions.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// configurar base de datos de lectura
builder.Services.AddDbContext<ReadDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=FlashBank_Read;Trusted_Connection=True;"));

//  worker de sincronizacion
builder.Services.AddHostedService<SyncWorker>();

var app = builder.Build();

app.MapControllers();

app.Run();