tools\vs-project-check.exe
tools\vs-build-all.exe -m -debug -release *
if %nopause%==true goto end
pause
:end