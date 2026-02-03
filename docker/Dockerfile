# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todo el proyecto
COPY . .

# Restaura dependencias y publica la app
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5039
ENTRYPOINT ["dotnet", "Pokemon.dll"]