# Usar la imagen base de .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Usar la imagen de SDK para construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivo de proyecto y restaurar dependencias
COPY ["Reto3_YD.csproj", "./"]
RUN dotnet restore "Reto3_YD.csproj"

# Copiar todo el código y compilar
COPY . .
RUN dotnet build "Reto3_YD.csproj" -c Release -o /app/build

# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "Reto3_YD.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagen final
FROM base AS final
WORKDIR /app

# Instalar herramientas de diagnóstico (opcional)
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copiar archivos publicados
COPY --from=publish /app/publish .

# Configurar variables de entorno
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Crear usuario no-root para seguridad
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Punto de entrada
ENTRYPOINT ["dotnet", "Reto3_YD.dll"]
