
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MCSApiController/MCSApiController.csproj", "MCSApiController/"]
COPY ["MCSApiInterface/MCSApiInterface.csproj", "MCSApiInterface/"]
COPY ["MCSApiData/MCSApiData.csproj", "MCSApiData/"]
RUN dotnet restore "MCSApiController/MCSApiController.csproj"
COPY . .
WORKDIR "/src/MCSApiController"
RUN dotnet build "MCSApiController.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MCSApiController.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MCSApiController.dll"]
