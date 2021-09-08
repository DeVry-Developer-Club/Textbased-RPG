FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TextbasedRPG/TextbasedRPG.csproj", "TextbasedRPG/"]
RUN dotnet restore "TextbasedRPG/TextbasedRPG.csproj"
COPY . .
WORKDIR "/src/TextbasedRPG"
RUN dotnet build "TextbasedRPG.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TextbasedRPG.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TextbasedRPG.dll"]
