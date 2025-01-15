# Use the official image as the base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Use the SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ManifestationApi.csproj ./
RUN dotnet restore "ManifestationApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "ManifestationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ManifestationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ManifestationApi.dll"]
