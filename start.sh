#!/bin/bash
# Script de inicio para Railway

# Aplicar migraciones si están disponibles
if [ "$ASPNETCORE_ENVIRONMENT" = "Production" ]; then
    echo "Aplicando migraciones en producción..."
    dotnet ef database update --no-build || echo "Error aplicando migraciones, continuando..."
fi

# Iniciar la aplicación
echo "Iniciando aplicación..."
exec dotnet Reto3_YD.dll
