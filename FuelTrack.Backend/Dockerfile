# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copiamos solo el .csproj primero
COPY FuelTrack.Backend.csproj ./FuelTrack.Backend.csproj

# Restauramos dependencias
RUN dotnet restore FuelTrack.Backend.csproj

# Copiamos el resto del proyecto
COPY . ./

# Publicamos la app
RUN dotnet publish FuelTrack.Backend.csproj -c Release -o /app

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FuelTrack.Backend.dll"]
