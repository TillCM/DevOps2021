# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

# install debug tools

#RUN apt-get install tree

#Environment Variables 
#ENV ConnectionStrings: "Data Source=db,1433;Database=teamfu;User Id=SA;Password=Your_password123;"

# copy csproj and restore as distinct layers
#copy unit test
COPY /com.teamfu.server/team_reece_tests ./
#RUN tree
RUN dotnet restore team_reece_tests.csproj
RUN ls -l
#copy project 
COPY ../src/com.teamfu.be/team-reece/team-reece.csproj ./
RUN dotnet restore team-reece.csproj

# copy everything else and build app
#build unit test 
COPY /com.teamfu.server/team_reece_tests/. /app/team-reece_tests/
WORKDIR /app/team_reece_tests
RUN mkdir /buildApp
ENV DATABASE_SERVER =db
RUN dotnet build team_reece_tests.csproj -c release -o /buildApp
RUN dotnet test "team_reece_tests.csproj" --logger "trx:LogFileName= Team_reece_tests.trx"
RUN ls


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
ENV TZ=Africa/Johannesburg
RUN apt-get update
RUN apt-get upgrade -y
RUN apt-get install -y tzdata curl
WORKDIR /app
COPY --from=build /publishedApp .

HEALTHCHECK CMD curl --fail http://localhost:80 || exit 1

ENTRYPOINT ["dotnet", "team-reece_tests.dll"]