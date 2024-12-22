FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /build
COPY ["src/JLG.Test.Server/JLG.Test.Server.csproj", "src/JLG.Test.Server/JLG.Test.Server.csproj"]
RUN dotnet restore "src/JLG.Test.Server/JLG.Test.Server.csproj"
COPY . .
RUN dotnet build "/build/src/JLG.Test.Server/JLG.Test.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS integrationtest
WORKDIR "/build/test/JLG.Test.Server.IntegrationTests/"

FROM build as unittest
WORKDIR "/build/test/JLG.Test.Server.UnitTests/"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "/build/src/JLG.Test.Server/JLG.Test.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JLG.Test.Server.dll"]