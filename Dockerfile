# Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Instalar dotnet-ef como herramienta global
RUN dotnet tool install --global dotnet-ef

# Asegurar que el PATH de herramientas esté en el entorno
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /src

# Copiar solo el csproj primero (para aprovechar el cache)
COPY SalonReservaApi.csproj ./
RUN dotnet restore

# Copiar el resto del código
COPY . .

# Publicar la aplicación
RUN dotnet publish -c Release -o /app/publish

# Etapa final: imagen más liviana para producción
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080

ENTRYPOINT ["dotnet", "SalonReservaApi.dll"]