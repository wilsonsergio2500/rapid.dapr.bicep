#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
RUN echo $(ls)
COPY ["Rapid.Dapr.Task/Rapid.Dapr.Task.csproj", "Rapid.Dapr.Task/"]
COPY ["Rapid.Dapr.Push/Rapid.Dapr.Push.csproj", "Rapid.Dapr.Push/"]
RUN dotnet restore "Rapid.Dapr.Push/Rapid.Dapr.Push.csproj"
COPY . .
WORKDIR "/src/Rapid.Dapr.Push"
RUN dotnet build "Rapid.Dapr.Push.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rapid.Dapr.Push.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rapid.Dapr.Push.dll"]