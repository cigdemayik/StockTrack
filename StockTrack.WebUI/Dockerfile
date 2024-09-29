# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StockTrack.WebUI/StockTrack.WebUI.csproj", "StockTrack.WebUI/"]
COPY ["StockTrack.Business/StockTrack.Business.csproj", "StockTrack.Business/"]
COPY ["StockTrack.DataAccess/StockTrack.DataAccess.csproj", "StockTrack.DataAccess/"]
COPY ["StockTrack.Entities/StockTrack.Entities.csproj", "StockTrack.Entities/"]
COPY ["StockTrack.Helpers/StockTrack.Helpers.csproj", "StockTrack.Helpers/"]
RUN dotnet restore "./StockTrack.WebUI/StockTrack.WebUI.csproj"
COPY . .
WORKDIR "/src/StockTrack.WebUI"
RUN dotnet build "./StockTrack.WebUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StockTrack.WebUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StockTrack.WebUI.dll"]