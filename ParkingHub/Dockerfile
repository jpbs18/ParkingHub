# Etapa base (runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Etapa build (SDK + tools)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar o projeto
COPY ["ParkingHub/ParkingHub.csproj", "ParkingHub/"]
RUN dotnet restore "ParkingHub/ParkingHub.csproj"

# Copiar o resto dos arquivos
COPY . .
WORKDIR "/src/ParkingHub"

# Instalar o CLI do Entity Framework dentro do container
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Compilar o projeto
RUN dotnet build "ParkingHub.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicar o app
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ParkingHub.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final (produção)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Garante que as ferramentas do EF estão acessíveis no runtime
ENV PATH="$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "ParkingHub.dll"]
