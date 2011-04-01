@echo off
rd /S /Q package
del /Q Solvent.xml
md package\bin
copy lib\AWSSDK.dll package\bin
call tools\package.exe "..\Solvent" "package" "/"
pause