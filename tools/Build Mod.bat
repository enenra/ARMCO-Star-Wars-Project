for %%I in (.) do set CurrDirName=%%~nxI
set ModDirName=%APPDATA%\SpaceEngineers\Mods\%CurrDirName%\
@RD /s "%ModDirName%"
xcopy /s /y "." "%ModDirName%" /exclude:exclude.txt
xcopy /s /y "..\tools\Upload to Workshop.bat" "%ModDirName%"
pause