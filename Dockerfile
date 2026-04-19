# 1. Etapa de construcción (Usa el SDK de .NET 8)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copiar el archivo del proyecto respetando la subcarpeta y restaurar dependencias
COPY ["NanoGuardian.Api/NanoGuardian.Api.csproj", "NanoGuardian.Api/"]
RUN dotnet restore "NanoGuardian.Api/NanoGuardian.Api.csproj"

# 3. Copiar el resto del código y compilar la aplicación
COPY . .
WORKDIR "/src/NanoGuardian.Api"
RUN dotnet publish "NanoGuardian.Api.csproj" -c Release -o /app/publish

# 4. Etapa de ejecución (Usa solo el entorno de ejecución, es más ligero)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 5. Configurar el puerto que usará Render
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# 6. Punto de entrada para encender la API
ENTRYPOINT ["dotnet", "NanoGuardian.Api.dll"]