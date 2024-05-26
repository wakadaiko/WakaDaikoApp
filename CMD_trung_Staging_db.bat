@ECHO OFF

CALL ECHO ----------------
CALL ECHO 1 - [ staging - database and migrations ]
CALL ECHO ----------------

SET /P input="ENTER: "

IF %input% == 1 (
    SET ASPNETCORE_ENVIRONMENT=Trung_Staging

    CALL dotnet ef database drop --force

    CALL dotnet ef database update
)

CALL ECHO ----------------

CALL ECHO FINISHED

PAUSE
