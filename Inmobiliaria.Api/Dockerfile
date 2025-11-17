FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Inmobiliaria.Api/Inmobiliaria.Api.csproj", "Inmobiliaria.Api/"]
COPY ["Inmobiliaria.Application/Inmobiliaria.Application.csproj", "Inmobiliaria.Application/"]
COPY ["Inmobiliaria.Domain/Inmobiliaria.Domain.csproj", "Inmobiliaria.Domain/"]
COPY ["Inmobiliaria.Infrastructure/Inmobiliaria.Infrastructure.csproj", "Inmobiliaria.Infrastructure/"]
RUN dotnet restore "Inmobiliaria.Api/Inmobiliaria.Api.csproj"
COPY . .
WORKDIR "/src/Inmobiliaria.Api"
RUN dotnet build "./Inmobiliaria.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Inmobiliaria.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inmobiliaria.Api.dll"]
