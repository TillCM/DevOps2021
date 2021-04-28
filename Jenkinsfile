pipeline {
    agent { docker { image 'mcr.microsoft.com/dotnet/sdk:5.0' } }
    stages {
        stage ('Git Checkout') {
          steps {
            git branch: "main", url: 'https://github.com/TillCM/teamfu'
          }
        }
        
        stage('Parallel Stage') {
            parallel {
                stage('restore and build') {
                    steps {
                        sh """
                            dotnet --version                 
                            dotnet restore ${workspace}/src/com.teamfu.be/team-reece/team-reece.csproj --disable-parallel
                            dotnet build ${workspace}/src/com.teamfu.be/team-reece/team-reece.csproj
                        """
                    }
                }
                stage('test') {
                    steps {
                        sh """
                            dotnet --version                 
                            dotnet restore ${workspace}/tests/com.teamfu.server/team_reece_tests/team_reece_tests.csproj --disable-parallel
                            dotnet test ${workspace}/tests/com.teamfu.server/team_reece_tests/team_reece_tests.csproj
                        """
                    }
                }
            }
        }
        
        stage('docker build, tag and push') {
            steps {
                sh '''
                    apt-get update -y
                    apt-get install -y lsb-release apt-transport-https ca-certificates curl gnupg lsb-release sudo
                    apt-get clean all
                    apt-get update -y
                    apt-get upgrade -y
                    echo $(lsb_release -cs)
                    curl -sL https://aka.ms/InstallAzureCLIDeb | sudo bash
                    curl -fsSL https://download.docker.com/linux/debian/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg
                    echo "deb [arch=amd64 signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/debian   $(lsb_release -cs) stable test edge" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
                    apt-get update -y
                    apt-get install docker-ce docker-ce-cli containerd.io -y
                    docker --version
                    docker build . -t team_reece/teamfu:latest
                    az --version
                '''
            }
        }
        
        stage('sonarqube') {
            steps {
                sh """
                    mkdir -p /usr/share/man/man1
                    DEBIAN_FRONTEND=noninteractive
                    apt-get update -y
                    apt-get upgrade -y
                    apt-get install git openssh-server default-jre-headless -y
                    dotnet --version
                    dotnet tool install --global dotnet-sonarscanner
                    export PATH="$PATH:/root/.dotnet/tools"
                    dotnet sonarscanner begin /k:"teamfu-sarina" /d:sonar.host.url="http://192.168.11.167:9000" /d:sonar.login="ff2497988364aa5d53eea2397e5a2155ac9d05fc"
                    dotnet build ${workspace}/src/com.teamfu.be/team-reece/team-reece.csproj
                    dotnet sonarscanner end /d:sonar.login="ff2497988364aa5d53eea2397e5a2155ac9d05fc"
                """
            }
        }
        stage('Integration tests') {
            steps {
                sh """
                    curl -L "https://github.com/docker/compose/releases/download/1.29.1/docker-compose-Linux-x86_64" -o /usr/local/bin/docker-compose
                    chmod +x /usr/local/bin/docker-compose
                    ln -s /usr/local/bin/docker-compose /usr/bin/docker-compose
                    apt-get install nodejs npm sed -y
                    npm install -g newman
                    docker-compose --version
                    newman --version
                    hostname -I
                    docker-compose -f quikstart/docker-compose.yml -f quikstart/docker-compose.override.yml down --remove-orphans
                    docker system prune -f
                    docker-compose -f quikstart/docker-compose.yml pull
                    docker-compose -f quikstart/docker-compose.yml -f quikstart/docker-compose.override.yml build
                    docker-compose -f quikstart/docker-compose.yml up -d
                    sleep 10
                    docker-compose -f quikstart/docker-compose.yml -f quikstart/docker-compose.override.yml up -d
                    sed -i 's/localhost/192.168.11.167/g' postman/team-reece.postman_environment.json
                    cat postman/team-reece.postman_environment.json
                    sleep 10
                    docker ps -a
                    newman run postman/Team-Reece-api.postman_collection.json -e postman/team-reece.postman_environment.json --insecure
                    docker-compose -f quikstart/docker-compose.yml -f quikstart/docker-compose.override.yml down --remove-orphans
                """
            }
        }
    }
}