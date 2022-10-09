#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5009

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Microservices/BookingService/API/TravixBackend.BookingService.API.csproj", "Microservices/BookingService/API/"]
COPY ["Microservices/BookingService/Domain/TravixBackend.BookingService.Domain.csproj", "Microservices/BookingService/Domain/"]
RUN dotnet restore "Microservices/BookingService/API/TravixBackend.BookingService.API.csproj"
COPY . .
WORKDIR "/src/Microservices/BookingService/API"
RUN dotnet build "TravixBackend.BookingService.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravixBackend.BookingService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravixBackend.BookingService.API.dll"]