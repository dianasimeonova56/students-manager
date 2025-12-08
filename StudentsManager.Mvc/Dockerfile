FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["StudentsManager.Mvc/StudentsManager.Mvc.csproj", "StudentsManager.Mvc/"]
RUN dotnet restore "StudentsManager.Mvc/StudentsManager.Mvc.csproj"
COPY . .
WORKDIR "/src/StudentsManager.Mvc"
RUN dotnet build "StudentsManager.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentsManager.Mvc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentsManager.Mvc.dll"]
