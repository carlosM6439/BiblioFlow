using Microsoft.EntityFrameworkCore;
using BiblioFlow.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configuración de PostgreSQL mediante Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// CONFIGURACIÓN DE CORS: Permite que la App Móvil ejecute peticiones sin ser bloqueada
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Habilitar política de CORS
app.UseCors("AllowAll");

// SE DESACTIVÓ LA REDIRECCIÓN HTTPS:
// Esto evita errores de conexión cuando el emulador envía peticiones por HTTP (http://10.0.2.2:5000)
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run("http://0.0.0.0:5000");