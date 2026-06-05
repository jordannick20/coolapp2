FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
COPY .env ./
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["GradesApp.csproj", "./"]
RUN dotnet restore "GradesApp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "GradesApp.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "GradesApp.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GradesApp.dll"]
