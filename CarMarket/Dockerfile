#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Server/CarMarket.Server.csproj", "Server/"]
COPY ["CarMarket.BusinessLogic/CarMarket.BusinessLogic.csproj", "CarMarket.BusinessLogic/"]
COPY ["CarMarket.Core/CarMarket.Core.csproj", "CarMarket.Core/"]
COPY ["CarMarket.Data/CarMarket.Data.csproj", "CarMarket.Data/"]
RUN dotnet restore "Server/CarMarket.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "CarMarket.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarMarket.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarMarket.Server.dll"]

RUN chmod +x ./entrypoint.sh
CMD /bin/bash ./entrypoint.sh