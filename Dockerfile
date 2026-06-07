# Dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copier les fichiers de projet
COPY ["Server/ETechEnergie.Server.csproj", "Server/"]
COPY ["Client/ETechEnergie.Client.csproj", "Client/"]
COPY ["Shared/ETechEnergie.Shared/ETechEnergie.Shared.csproj", "Shared/"]

# Restaurer les dépendances
RUN dotnet restore "Server/ETechEnergie.Server.csproj"

# Copier tout le code source
COPY . .

# Build
WORKDIR "/src/Server"
RUN dotnet build "ETechEnergie.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ETechEnergie.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Variables d'environnement par défaut
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "ETechEnergie.Server.dll"]