pipeline {
    agent { docker { image 'mcr.microsoft.com/dotnet/sdk:5.0' } }
    stages {
        stage ('Git Checkout') {
          steps {
            git branch: "main", url: 'https://github.com/TillCM/teamfu'
          }
        }
        stage('restore') {
            steps {
                sh """
                    dotnet --version                 
                    dotnet restore ${workspace}/src/com.teamfu.be/team-reece/team-reece.csproj --disable-parallel
                    dotnet build ${workspace}/src/com.teamfu.be/team-reece/team-reece.csproj
                    dotnet test ${workspace}/src/com.teamfu.be/team-reece/team-reece.csproj --no-build
                """
            }
        }
    }
}