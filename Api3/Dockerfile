FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copia solo el archivo .csproj primero y restaura
COPY Api3.csproj ./
RUN dotnet restore Api3.csproj

# Copia el resto de los archivos
COPY . .

# Publica el proyecto
RUN dotnet publish Api3.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Api3.dll"]

