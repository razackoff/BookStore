FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BookStore.csproj", "./"]
ENV UPLOADS_FOLDER /app/uploads
RUN dotnet restore "BookStore.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "BookStore.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookStore.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
COPY wwwroot/images /app/images
COPY wwwroot/files /app/files

ENTRYPOINT ["dotnet", "BookStore.dll"]
