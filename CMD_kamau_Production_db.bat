@ECHO OFF

ECHO ----------------
ECHO 1 - [ production - database and migrations ]
ECHO ----------------

SET /P input="ENTER: "

IF %input% == 1 (
    SET ASPNETCORE_ENVIRONMENT=Production

    dotnet ef database drop --force

    dotnet ef database update
)

ECHO ----------------

ECHO FINISHED

PAUSE
