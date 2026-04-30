# Imagen de SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Basado en tus fotos: la carpeta se llama NanoGuardian.Api
# Copiamos el archivo .csproj primero para restaurar dependencias
COPY ["NanoGuardian.Api/NanoGuardian.Api.csproj", "NanoGuardian.Api/"]
RUN dotnet restore "NanoGuardian.Api/NanoGuardian.Api.csproj"

# Copiamos todo el contenido de la raíz al contenedor
COPY . ./

# Publicamos el proyecto especificando la ruta del csproj
RUN dotnet publish "NanoGuardian.Api/NanoGuardian.Api.csproj" -c Release -o out

# Imagen de runtime para ejecutar la app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Render usa el puerto 80 por defecto en contenedores
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

# El nombre del DLL debe coincidir con el nombre del proyecto compilado
ENTRYPOINT ["dotnet", "NanoGuardian.Api.dll"]