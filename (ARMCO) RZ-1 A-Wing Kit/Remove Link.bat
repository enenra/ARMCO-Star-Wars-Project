for %%I in (.) do set CurrDirName=%%~nxI
rmdir "%APPDATA%\SpaceEngineers\Mods\%CurrDirName%"
pause