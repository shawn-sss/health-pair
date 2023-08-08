FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln ./
COPY HealthPairAPI/*.csproj HealthPairAPI/
COPY HealthPairDomain/*.csproj HealthPairDomain/
COPY HealthPairDataAccess/*.csproj HealthPairDataAccess/
COPY HealthPairTests/*.csproj HealthPairTests/
RUN dotnet restore

COPY . ./
RUN dotnet publish HealthPairAPI -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HealthPairAPI.dll"]
