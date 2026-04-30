# 1. SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiamos el archivo del proyecto (estando en la misma carpeta que el Dockerfile)
COPY "NanoGuardian.Api.csproj" ./
RUN dotnet restore

# Copiamos el resto de los archivos
COPY . ./

# Publicamos
RUN dotnet publish "NanoGuardian.Api.csproj" -c Release -o out

# 2. Runtime para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configuración de puerto para Render
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "NanoGuardian.Api.dll"]