################################################################################
##  File:  Install-NSIS.ps1
##  Desc:  Install NSIS
################################################################################

Invoke-WebRequest "https://netcologne.dl.sourceforge.net/project/nsis/NSIS%203/3.04/nsis-3.04-setup.exe" -OutFile "C:\WINDOWS\Temp\nsis-3.04-setup.exe"
Invoke-Expression "& C:\WINDOWS\Temp\nsis-3.04-setup.exe /S"

$NsisPath = "${env:ProgramFiles(x86)}\NSIS\"
Add-MachinePathItem $NsisPath
$env:Path = Get-MachinePath