tools\vs-project-check.exe
tools\vs-build-all.exe -m -debug *
if %nopause%==true goto end
pause
:end