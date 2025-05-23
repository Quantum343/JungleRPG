@echo off
powershell -ExecutionPolicy Bypass -Command "& {git add .; $msg=Read-Host 'Enter commit message'; git commit -m $msg; git push origin main}"
