FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ["TicketsManagement.Api/TicketsManagement.Api.csproj", "TicketsManagement.Api/"]
COPY ["TicketsManagement.Infrastructure/TicketsManagement.Infrastructure.csproj", "TicketsManagement.Infrastructure/"]
COPY ["TicketsManagement.Application/TicketsManagement.Application.csproj", "TicketsManagement.Application/"]
COPY ["TicketsManagement.Domain/TicketsManagement.Domain.csproj", "TicketsManagement.Domain/"]

RUN dotnet restore "TicketsManagement.Api/TicketsManagement.Api.csproj"


COPY . .

WORKDIR "/src/TicketsManagement.Api"
RUN dotnet build "TicketsManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TicketsManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TicketsManagement.Api.dll"]
