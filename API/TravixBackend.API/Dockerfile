#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 3003

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["../TravixBackend.API/TravixBackend.API.csproj", "../TravixBackend.API/"]
RUN dotnet restore "../TravixBackend.API/TravixBackend.API.csproj"
COPY . .
WORKDIR "/src/../TravixBackend.API"
RUN dotnet build "TravixBackend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravixBackend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravixBackend.API.dll"]