#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

COPY ["src/SalesManagement.Report/SalesManagement.Report.Api/SalesManagement.Report.Api.csproj", "SalesManagement.Report.Api/"]
COPY ["src/SalesManagement.Report/SalesManagement.Report.Application/SalesManagement.Report.Application.csproj", "SalesManagement.Report.Application/"]
COPY ["src/SalesManagement.Report/SalesManagement.Report.Domain/SalesManagement.Report.Domain.csproj", "SalesManagement.Report.Domain/"]
COPY ["src/SalesManagement.Report/SalesManagement.Report.Infrastructure/SalesManagement.Report.Infrastructure.csproj", "src/SalesManagement.Report.Infrastructure/"]

RUN dotnet restore "SalesManagement.Report.Api/SalesManagement.Report.Api.csproj"

COPY ./src/SalesManagement.Report /source
WORKDIR "/source/SalesManagement.Report.Api"
RUN dotnet build "SalesManagement.Report.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesManagement.Report.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesManagement.Report.Api.dll"]