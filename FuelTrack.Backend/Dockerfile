# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos el archivo .csproj primero para aprovechar la caché
COPY FuelTrack.Backend.csproj ./
RUN dotnet restore FuelTrack.Backend.csproj

# Copiamos el resto del código
COPY . ./
RUN dotnet publish -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FuelTrack.Backend.dll"]
