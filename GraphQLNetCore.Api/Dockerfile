#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["GraphQLNetCore.Api/GraphQLNetCore.Api.csproj", "GraphQLNetCore.Api/"]
RUN dotnet restore "GraphQLNetCore.Api/GraphQLNetCore.Api.csproj"
COPY . .
WORKDIR "/src/GraphQLNetCore.Api"
RUN dotnet build "GraphQLNetCore.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GraphQLNetCore.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GraphQLNetCore.Api.dll"]