# Imagen de SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copiamos el archivo .csproj usando la ruta relativa correcta
COPY ["NanoGuardian.Api/NanoGuardian.Api.csproj", "NanoGuardian.Api/"]
RUN dotnet restore "NanoGuardian.Api/NanoGuardian.Api.csproj"

# Copiamos todo el código fuente
COPY . .

# Compilamos y publicamos
RUN dotnet publish "NanoGuardian.Api/NanoGuardian.Api.csproj" -c Release -o out

# Imagen de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Configuración de puerto para Render
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

ENTRYPOINT ["dotnet", "NanoGuardian.Api.dll"]