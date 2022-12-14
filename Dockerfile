FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Biblioteca-Server.csproj", "./"]
RUN dotnet restore "Biblioteca-Server.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Biblioteca-Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Biblioteca-Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Biblioteca-Server.dll"]
