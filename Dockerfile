# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src


COPY *.csproj ./
RUN dotnet restore 

COPY . .
RUN dotnet publish "Pokemon.csproj" -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5039
ENTRYPOINT ["dotnet", "Pokemon.dll"]
