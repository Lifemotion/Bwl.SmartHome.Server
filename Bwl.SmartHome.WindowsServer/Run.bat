SET USER     = "Administrator"
SET PASSWORD = "qwerty"
SET WEB_URL  = "http/*:80:"

dism /online /enable-feature /featurename:netfx3
powershell.exe install-WindowsFeature -ConfigurationFilePath ServerConfig.xml
%windir%\system32\inetsrv\APPCMD delete site "Default Web Site" 
%windir%\system32\inetsrv\APPCMD add site /name:SmartHome /bindings:%WEB_URL% /physicalPath:"C:\SmartHomeWeb"
mkdir C:\SmartHomeWeb
msiexec /I deploy64.msi /passive ADDLOCAL=ALL LISTENURL=http://+:8172/deploy/
net start msdepsvc
net start wmsvc
netsh advfirewall firewall add rule name="RMHC" dir=in action=allow protocol=TCP localport=8064
netsh advfirewall firewall add rule name="SmartHome" dir=in action=allow protocol=TCP localport=3210
dism /online /enable-feature /all /featurename:IIS-ASPNET45
reg add "HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v AutoAdminLogon /t REG_SZ /d 1 /f
reg add "HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v DefaultUserName /t REG_SZ /d %USER% /f
reg add "HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon" /v DefaultPassword /t REG_SZ /d %PASSWORD% /f
schtasks /create /tn "Start RMHC" /tr "c:\rmhc\bin\Bwl.Tools.RunMonitorPlatform.HostControl.exe localserver=8064 remoteapp" /sc onlogon 
xcopy /s rmhc c:\rmhc\
xcopy /s web C:\SmartHomeWeb
shutdown -t 0 -r -f

