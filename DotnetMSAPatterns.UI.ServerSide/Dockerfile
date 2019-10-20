FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["DotnetMSAPatterns.UI.ServerSide.csproj", ""]
RUN dotnet restore "./DotnetMSAPatterns.UI.ServerSide.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "DotnetMSAPatterns.UI.ServerSide.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DotnetMSAPatterns.UI.ServerSide.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetMSAPatterns.UI.ServerSide.dll"]