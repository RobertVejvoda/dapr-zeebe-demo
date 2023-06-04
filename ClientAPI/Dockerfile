FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS http://*:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ClientAPI/ClientAPI.csproj", "ClientAPI/"]
RUN dotnet restore "ClientAPI/ClientAPI.csproj"
COPY . .
WORKDIR "/src/ClientAPI"
RUN dotnet build "ClientAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientAPI.dll"]
