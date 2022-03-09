SET Environment=%1
IF "%Environment%" == "" SET Environment=Development

REM Install node packages
cd client
call yarn install

REM Run front watcher
start cmd /k yarn watch

REM Run back
cd ..\server
start "CuratedArt Back" dotnet run --environment %Environment%

exit