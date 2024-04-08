@ECHO off

ECHO ----------------
ECHO 1 - CALL dotnet watch run
ECHO 2 - [ local - reset and update database / migrations ]
ECHO 3 - CALL dotnet build
ECHO 4 - CALL dotnet test
ECHO 8 - [ remote - update database ]
ECHO 10 - CALL git reset --soft HEAD~1
ECHO 11 - [ git - sync main branch with Branch1-KG branch ]
ECHO 12 - [ git - sync main branch with Trung branch ]
ECHO 13 - [ git - sync Branch1-KG branch with Trung branch ]
ECHO 14 - [ git - sync Trung branch with Branch1-KG branch ]
ECHO ----------------

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

IF %input% == 8 (
    SET ASPNETCORE_ENVIRONMENT=Production

    CALL dotnet ef database drop --force

    CALL dotnet ef database update
)

IF %input% == 10 (
	CALL git reset --soft HEAD~1
)


IF %input% == 11 (
	git checkout main
    git pull
    git checkout Branch1-KG
    git merge main
)

IF %input% == 12 (
	git checkout main
    git pull
    git checkout Trung
    git merge main
)

IF %input% == 13 (
	git checkout main
    git pull
    git checkout Branch1-KG
    git merge Trung
)

IF %input% == 14 (
	git checkout main
    git pull
    git checkout Trung
    git merge Branch1-KG
)

ECHO ----------------

ECHO FINISHED

PAUSE
