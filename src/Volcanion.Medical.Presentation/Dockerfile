ARG NUGET_SOURCE="http://10.99.81.15:5000/v3/index.json"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY volcanion-identity.sln ./
COPY src/Volcanion.Medical.Handlers/Volcanion.Medical.Handlers.csproj src/Volcanion.Medical.Handlers/
COPY src/Volcanion.Medical.Infrastructure/Volcanion.Medical.Infrastructure.csproj src/Volcanion.Medical.Infrastructure/
COPY src/Volcanion.Medical.Models/Volcanion.Medical.Models.csproj src/Volcanion.Medical.Models/
COPY src/Volcanion.Medical.Services/Volcanion.Medical.Services.csproj src/Volcanion.Medical.Services/
COPY src/Volcanion.Medical.Presentation/Volcanion.Medical.Presentation.csproj src/Volcanion.Medical.Presentation/
COPY src/Volcanion.Medical.Presentation/Secrets/* src/Volcanion.Medical.Presentation/Secrets/

RUN dotnet restore --source "https://api.nuget.org/v3/index.json" --source "$NUGET_SOURCE"

COPY . .

WORKDIR /app/src/Volcanion.Medical.Presentation
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /out .

ENTRYPOINT ["dotnet", "Volcanion.Medical.Presentation.dll"]
