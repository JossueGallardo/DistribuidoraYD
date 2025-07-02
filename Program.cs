using Microsoft.EntityFrameworkCore;
using Reto3_YD.Data;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Configurar cultura para manejar decimales correctamente
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { new CultureInfo("en-US") };
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers(); // Agregar controladores para la API

// Configurar base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Usar PostgreSQL en producción (Railway) y SQL Server en desarrollo
    if (builder.Environment.IsProduction())
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
        
        if (!string.IsNullOrEmpty(connectionString) && connectionString.StartsWith("postgresql://"))
        {
            // Convertir URL de PostgreSQL a connection string
            var uri = new Uri(connectionString);
            var host = uri.Host;
            var port = uri.Port;
            var database = uri.LocalPath.TrimStart('/');
            var userInfo = uri.UserInfo.Split(':');
            var username = userInfo[0];
            var password = userInfo.Length > 1 ? userInfo[1] : "";
            
            connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Prefer;Trust Server Certificate=true";
        }
        
        if (!string.IsNullOrEmpty(connectionString))
        {
            options.UseNpgsql(connectionString);
        }
        else
        {
            throw new InvalidOperationException("No se pudo configurar la base de datos. Variable DATABASE_URL no encontrada.");
        }
    }
    else
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});

// Configurar CORS para producción
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Aplicar migraciones automáticamente en producción con mejor manejo de errores
if (app.Environment.IsProduction())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        
        logger.LogInformation("Aplicando migraciones de base de datos...");
        context.Database.Migrate();
        logger.LogInformation("Migraciones aplicadas exitosamente.");
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error crítico aplicando migraciones: {Message}", ex.Message);
        // No lanzar excepción para que la app pueda iniciarse
    }
}

// Aplicar configuración de localización
app.UseRequestLocalization();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseCors("PermitirTodo");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

// Agregar health check para Railway
app.MapGet("/health", () => "OK");
app.MapGet("/", () => "Distribuidora YD API está funcionando!");

// Configurar puerto dinámico para Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var urls = $"http://0.0.0.0:{port}";

app.Logger.LogInformation("Iniciando aplicación en: {Urls}", urls);
app.Urls.Add(urls);

app.Run();
