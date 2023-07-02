# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim AS build
WORKDIR /src
EXPOSE 80

# copy csproj and restore as distinct layers
COPY "./ClientAPI/ClientAPI.csproj" .
RUN dotnet restore --use-current-runtime  

# copy everything else and build app
COPY "./ClientAPI/." .
RUN dotnet publish -o /app --use-current-runtime --no-self-contained --no-restore 

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ClientAPI.dll"]