#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SalesManagement.CashFlow.Api/SalesManagement.CashFlow.Api.csproj", "SalesManagement.CashFlow.Api/"]
RUN dotnet restore "SalesManagement.CashFlow.Api/SalesManagement.CashFlow.Api.csproj"
COPY . .
WORKDIR "/src/SalesManagement.CashFlow.Api"
RUN dotnet build "SalesManagement.CashFlow.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SalesManagement.CashFlow.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SalesManagement.CashFlow.Api.dll"]