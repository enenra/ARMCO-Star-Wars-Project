for %%I in (.) do set CurrDirName=%%~nxI
mklink /d /j "%APPDATA%\SpaceEngineers\Mods\%CurrDirName%" "..\%CurrDirName%"
pause