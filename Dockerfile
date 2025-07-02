# Usar la imagen base de .NET 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Usar la imagen de SDK para construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Reto3_YD.csproj", "."]
RUN dotnet restore "./Reto3_YD.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Reto3_YD.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Reto3_YD.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reto3_YD.dll"]
