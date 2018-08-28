set SEInstallDir="C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers"
for %%I in (.) do set CurrDirName=%%~nxI
%SEInstallDir%\Bin64\SEWorkshopTool.exe --mods "%CurrDirName%" --thumb "%CurrDirName%\thumb.jpg"
pause