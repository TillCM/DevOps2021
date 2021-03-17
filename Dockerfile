# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# install debug tools

#RUN apt-get install tree

#Environment Variables 
#ENV ConnectionStrings: "Data Source=db,1433;Database=teamfu;User Id=SA;Password=Your_password123;"

# copy csproj and restore as distinct layers
COPY src/com.teamfu.be/team-reece/team-reece.csproj ./
#RUN tree
RUN dotnet restore team-reece.csproj
RUN ls -l

# copy everything else and build app
COPY src/com.teamfu.be/team-reece/. /app/team-reece/
WORKDIR /app/team-reece
RUN mkdir /publishedApp
RUN dotnet publish team-reece.csproj -c release -o /publishedApp

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
ENV TZ=Africa/Johannesburg
RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install -y tzdata curl
WORKDIR /app
COPY --from=build /publishedApp .

HEALTHCHECK CMD curl --fail http://localhost:80 || exit 1

ENTRYPOINT ["dotnet", "team-reece.dll"]