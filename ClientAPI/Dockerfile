# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
# https://github.com/dotnet/dotnet-docker/blob/main/samples/aspnetapp/Dockerfile.alpine-non-root

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-preview-alpine AS build
ARG TARGETARCH
WORKDIR /src
EXPOSE 80

# copy csproj and restore as distinct layers
COPY "./ClientAPI/ClientAPI.csproj" .
RUN dotnet restore -a $TARGETARCH

# copy everything else and build app
COPY "./ClientAPI/." .
RUN dotnet publish -a $TARGETARCH -o /app --no-restore 

# Enable globalization and time zones:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/enable-globalization.md
# final stage/image
# .NET 8 tag includes `-preview`
FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview-alpine
WORKDIR /app

COPY --from=build /app .

USER $APP_UID
ENTRYPOINT ["./ClientAPI"]

