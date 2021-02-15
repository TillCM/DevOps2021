# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /appv2


# copy csproj and restore as distinct layers
COPY src/com.teamfu.be/team-reece/*.csproj ./
RUN dotnet restore 

# copy everything else and build app
COPY src/com.teamfu.be/team-reece/. ./team-reece/
WORKDIR /appv2/team-reece
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /appv2
COPY --from=build /appv2 ./
ENTRYPOINT ["dotnet", "team-reece.dll"]