#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["GRPC/GRPC.csproj", "GRPC/"]
COPY ["Dto/Dto.csproj", "Dto/"]
COPY ["Services/Services.csproj", "Services/"]
RUN dotnet restore "GRPC/GRPC.csproj"
COPY . .
WORKDIR "/src/GRPC"
RUN dotnet build "GRPC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GRPC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GRPC.dll"]