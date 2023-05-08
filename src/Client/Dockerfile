FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BabySounds/Client/BabySounds.Client.csproj", "src/BabySounds/Client/"]
COPY ["src/BabySounds/Shared/BabySounds.Shared.csproj", "src/BabySounds/Shared/"]
RUN dotnet restore "src/BabySounds/Client/BabySounds.Client.csproj"
COPY . .
WORKDIR "/src/src/BabySounds/Client"
RUN dotnet build "BabySounds.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BabySounds.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BabySounds.Client.dll"]
