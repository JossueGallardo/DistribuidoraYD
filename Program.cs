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

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // Usar PostgreSQL en producción (Railway) y SQL Server en desarrollo
    if (builder.Environment.IsProduction())
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
                             ?? builder.Configuration.GetConnectionString("DefaultConnection");
        
        // Si es una URL de Railway/PostgreSQL, convertirla al formato de connection string
        if (connectionString?.StartsWith("postgresql://") == true)
        {
            var uri = new Uri(connectionString);
            var host = uri.Host;
            var port = uri.Port;
            var database = uri.LocalPath.TrimStart('/');
            var username = uri.UserInfo.Split(':')[0];
            var password = uri.UserInfo.Split(':')[1];
            
            connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
        }
        
        options.UseNpgsql(connectionString);
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
        if (builder.Environment.IsDevelopment())
        {
            // En desarrollo, permitir cualquier origen
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            // En producción, permitir Railway y otros orígenes
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    });
});

var app = builder.Build();

// Aplicar migraciones automáticamente en producción
if (app.Environment.IsProduction())
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        try
        {
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Error aplicando migraciones");
        }
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

// Comentar esta línea para evitar redirección forzada a HTTPS
// app.UseHttpsRedirection();

app.UseCors("PermitirTodo"); // Aplicar CORS antes de routing
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers(); // Mapear controladores de API

// Configurar puerto dinámico para Railway
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
