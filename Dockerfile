FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app
COPY ./StockTrack.Business/*.csproj ./StockTrack.Business/
COPY ./StockTrack.DataAccess/*.csproj ./StockTrack.DataAccess/
COPY ./StockTrack.Entities/*.csproj ./StockTrack.Entities/
COPY ./StockTrack.Helpers/*.csproj ./StockTrack.Helpers/
COPY ./StockTrack.WebUI/*.csproj ./StockTrack.WebUI/

COPY *.sln . 
RUN dotnet restore
COPY . .
RUN dotnet publish ./StockTrack.WebUI/*.csproj -o  /publish/
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet", "StockTrack.WebUI.dll" ]





