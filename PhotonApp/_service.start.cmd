@echo off
REM change dir to location of script
SET mypath=%~dp0
CD %mypath%

echo Starting Photon service at: 
WMIC service "Photon Socket Server: ServerGame" get Pathname

SC start "Photon Socket Server: ServerGame"
pause
