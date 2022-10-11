#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5010

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/UserService/API/TravixBackend.UserService.API.csproj", "Microservices/UserService/API/"]
COPY ["Microservices/UserService/Domain/TravixBackend.UserService.Domain.csproj", "Microservices/UserService/Domain/"]
RUN dotnet restore "Microservices/UserService/API/TravixBackend.UserService.API.csproj"
COPY . .
WORKDIR "/src/Microservices/UserService/API"
RUN dotnet build "TravixBackend.UserService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravixBackend.UserService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravixBackend.UserService.API.dll"]