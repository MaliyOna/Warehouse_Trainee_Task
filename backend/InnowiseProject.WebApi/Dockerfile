FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InnowiseProject.WebApi/InnowiseProject.WebApi.csproj", "InnowiseProject.WebApi/"]
COPY ["InnowiseProject.Database/InnowiseProject.Database.csproj", "InnowiseProject.Database/"]
COPY ["InnowiseProject.Application/InnowiseProject.Application.csproj", "InnowiseProject.Application/"]
RUN dotnet restore "InnowiseProject.WebApi/InnowiseProject.WebApi.csproj"
COPY . .
WORKDIR "/src/InnowiseProject.WebApi"
RUN dotnet build "InnowiseProject.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InnowiseProject.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnowiseProject.WebApi.dll"]