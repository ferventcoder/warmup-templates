try {
  Get-ChildItem 'C:\NuGet\chocolateyInstall\helpers' -Filter *.psm1 | ForEach-Object { import-module -name  $_.FullName }
  Install-ChocolateyPackage '__NAME__' 'EXE_OR_MSI' 'SILENT_ARGS' 'URL' '64BIT_URL_REPEAT_32BIT_IF_NO_64BIT' 
  #"/s /S /q /Q /quiet /silent /SILENT /VERYSILENT" # try any of these to get the silent installer
  #msi is always /quiet

#------- ADDITIONAL SETUP -------#
#$processor = Get-WmiObject Win32_Processor
#$is64bit = $processor.AddressWidth -eq 64

# stuff here?

#write-host "__NAME__ has been installed."
#Start-Sleep 6

} catch {
  Start-Sleep 10
}