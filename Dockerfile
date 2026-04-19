# 1. Etapa de construcción
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. Copiar tu proyecto y restaurar
COPY ["NanoGuardian.Api.csproj", "./"]
RUN dotnet restore "NanoGuardian.Api.csproj"

# 3. Copiar el resto y compilar
COPY . .
RUN dotnet publish "NanoGuardian.Api.csproj" -c Release -o /app/publish

# 4. Etapa de ejecución (Servidor en Render)
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# 5. Configurar puertos
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# 6. Encender la API
ENTRYPOINT ["dotnet", "NanoGuardian.Api.dll"]