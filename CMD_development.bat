@ECHO OFF

CALL ECHO ----------------
CALL ECHO 1 - [ development - run ]
CALL ECHO 2 - [ development - database and migrations ]
CALL ECHO 3 - [ development - build ]
CALL ECHO 4 - [ development - test ]
CALL ECHO 10 - [ git - merge branches - main to kamaug ]
CALL ECHO 11 - [ git - merge branches - main to Trung ]
CALL ECHO 12 - [ git - remove - last commit ]
CALL ECHO ----------------

SET /P input="ENTER: "

IF %input% == 1 (
	CALL dotnet watch run
)

IF %input% == 2 (
    SET ASPNETCORE_ENVIRONMENT=Development

    CALL dotnet ef database drop --force

    CALL dotnet ef migrations remove

    CALL dotnet ef migrations add MySQL

    CALL dotnet ef database update
)

IF %input% == 3 (
    CALL dotnet build
)

IF %input% == 4 (
     CALL CD %CD%\Tests\Unit
    @REM CALL dotnet test
     CALL dotnet watch test --logger:"console;verbosity=detailed"
)

IF %input% == 10 (
    CALL git checkout kamaug
    CALL git merge main
)

IF %input% == 11 (
    CALL git checkout Trung
    CALL git merge main
)

IF %input% == 12 (
	CALL git reset --soft HEAD~1
)

CALL ECHO ----------------

CALL ECHO FINISHED

PAUSE
