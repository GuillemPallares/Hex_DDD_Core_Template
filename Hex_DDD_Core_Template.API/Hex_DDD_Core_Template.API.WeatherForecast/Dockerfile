FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5279
EXPOSE 7112

ENV ASPNETCORE_URLS=http://+:5279

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Hex_DDD_Core_Template.API/Hex_DDD_Core_Template.API.WeatherForecast/Hex_DDD_Core_Template.API.WeatherForecast.csproj", "Hex_DDD_Core_Template.API/Hex_DDD_Core_Template.API.WeatherForecast/"]
RUN dotnet restore "Hex_DDD_Core_Template.API/Hex_DDD_Core_Template.API.WeatherForecast/Hex_DDD_Core_Template.API.WeatherForecast.csproj"
COPY . .
WORKDIR "/src/Hex_DDD_Core_Template.API/Hex_DDD_Core_Template.API.WeatherForecast"
RUN dotnet build "Hex_DDD_Core_Template.API.WeatherForecast.csproj" -c Release -o /app/build/api

FROM build AS publish
RUN dotnet publish "Hex_DDD_Core_Template.API.WeatherForecast.csproj" -c Release -o /app/publish/api /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish/api .
ENTRYPOINT ["dotnet", "Hex_DDD_Core_Template.API.WeatherForecast.dll"]
