################################################################################
##  File:  Install-NSIS.ps1
##  Desc:  Install NSIS
################################################################################

$NsisVersion = "3.04"
$downloadUrl = "https://netcologne.dl.sourceforge.net/project/nsis/NSIS%203/${NsisVersion}/nsis-${NsisVersion}-setup.exe"
$downloadPath = "C:\WINDOWS\Temp\nsis-${NsisVersion}-setup.exe"

Write-Host "Downloading NSIS version $NsisVersion"
$response = Invoke-WebRequest $downloadUrl -OutFile $downloadPath -PassThru
Write-Host "Downloaded $($response.BaseResponse.ContentLength) bytes"

Write-Host "Installing NSIS version $NsisVersion"
$installOutput = Invoke-Expression "& $downloadPath /S 2>&1"
Write-Host "Install Output: $installOutput"

$NsisPath = "${env:ProgramFiles(x86)}\NSIS\"
Write-Host "Adding NSIS to the PATH"
Add-MachinePathItem $NsisPath
$env:Path = Get-MachinePath

Write-Host "Checking NSIS version"
$versionOutput = makensis.exe /VERSION
Write-Host "NSIS Version: $versionOutput"