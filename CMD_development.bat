@ECHO off

ECHO ----------------
ECHO 1 - CALL dotnet watch run
ECHO 2 - [ local - reset and update database / migrations ]
ECHO 3 - CALL dotnet build
ECHO 4 - CALL dotnet test
ECHO 8 - [ remote - update database ]
ECHO 10 - CALL git reset --soft HEAD~1
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
	CALL CALL git reset --soft HEAD~1
)

ECHO ----------------

ECHO FINISHED

PAUSE
