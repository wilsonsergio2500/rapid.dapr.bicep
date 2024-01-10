FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# SQL Server trusted connection problem
RUN sed -i 's/CipherString = DEFAULT@SECLEVEL=2/CipherString = DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Rapid.Dapr.Task/Rapid.Dapr.Task.csproj", "Rapid.Dapr.Task/"]
COPY ["FoxPoint.MailQueue/FoxPoint.MailQueue.csproj", "FoxPoint.MailQueue/"]

COPY ["Rapid.Dapr.Sub/Rapid.Dapr.Sub.csproj", "Rapid.Dapr.Sub/"]
RUN dotnet restore "Rapid.Dapr.Sub/Rapid.Dapr.Sub.csproj"
COPY . .
WORKDIR "/src/Rapid.Dapr.Sub"
RUN dotnet build "Rapid.Dapr.Sub.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Rapid.Dapr.Sub.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Rapid.Dapr.Sub.dll"]
