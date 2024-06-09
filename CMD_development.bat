@ECHO OFF

ECHO ----------------
ECHO 1 - [ development - run ]
ECHO 2 - [ development - database and migrations ]
ECHO 3 - [ development - build ]
ECHO 4 - [ development - test ]
ECHO 10 - [ git - merge branches - main to kamaug ]
ECHO 11 - [ git - merge branches - main to Trung ]
ECHO 12 - [ git - remove - last commit ]
ECHO ----------------

SET /P input="ENTER: "

IF %input% == 1 (
	dotnet watch run
)

IF %input% == 2 (
    SET ASPNETCORE_ENVIRONMENT=Development

    dotnet ef database drop --force

    dotnet ef migrations remove

    dotnet ef migrations add MySQL

    dotnet ef database update
)

IF %input% == 3 (
    dotnet build
)

IF %input% == 4 (
    CD %CD%\Tests\Unit
    dotnet test
)

IF %input% == 10 (
    git checkout kamaug
    git merge main
)

IF %input% == 11 (
    git checkout Trung
    git merge main
)

IF %input% == 12 (
	git reset --soft HEAD~1
)

ECHO ----------------

ECHO FINISHED

PAUSE
