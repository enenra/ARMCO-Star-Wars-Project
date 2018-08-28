for %%I in (.) do set CurrDirName=%%~nxI
echo %CurrDirName%
set ModDirName=%APPDATA%\SpaceEngineers\Mods\%CurrDirName%\
@RD /s "%ModDirName%"
xcopy /s /y "." "%ModDirName%" /exclude:exclude.txt
pause