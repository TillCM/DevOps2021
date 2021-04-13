#!/bin/bash
set -eu -o pipefail

dotnet restore /tests/com.teamfu.server/team_reece_tests/team_reece_tests.csproj
dotnet test /tests/com.teamfu.server/team_reece_tests/team_reece_tests.csproj