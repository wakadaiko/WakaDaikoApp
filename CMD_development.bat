@ECHO off

CALL ECHO ----------------
CALL ECHO 1 - [ development - run ]
CALL ECHO 2 - [ development - database and migrations ]
CALL ECHO 3 - [ development - build ]
CALL ECHO 4 - [ development - test ]
CALL ECHO 10 - [ git - sync main branch with Branch1-KG branch ]
CALL ECHO 11 - [ git - sync main branch with Trung branch ]
CALL ECHO 12 - [ git - sync Branch1-KG branch with Trung branch ]
CALL ECHO 13 - [ git - sync Trung branch with Branch1-KG branch ]
CALL ECHO 14 - [ git - remove last commit ]
CALL ECHO ----------------

SET /P input="ENTER: "

IF %input% == 1 (
	CALL dotnet watch run
)

IF %input% == 2 (
    SET ASPNETCORE_ENVIRONMENT=Development

    CALL dotnet ef database drop --force

    CALL dotnet ef migrations remove

    CALL dotnet ef migrations add Comment

    CALL dotnet ef database update
)

IF %input% == 3 (
    CALL dotnet build
)

IF %input% == 4 (
    CALL dotnet test
)

IF %input% == 10 (
	CALL git checkout main
    CALL git pull
    CALL git checkout Branch1-KG
    CALL git merge main
)

IF %input% == 11 (
	CALL git checkout main
    CALL git pull
    CALL git checkout Trung
    CALL git merge main
)

IF %input% == 12 (
	CALL git checkout main
    CALL git pull
    CALL git checkout Branch1-KG
    CALL git merge Trung
)

IF %input% == 13 (
	CALL git checkout main
    CALL git pull
    CALL git checkout Trung
    CALL git merge Branch1-KG
)

IF %input% == 14 (
	CALL git reset --soft HEAD~1
)

CALL ECHO ----------------

CALL ECHO FINISHED

PAUSE
