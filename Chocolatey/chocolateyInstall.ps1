$ErrorActionPreference = 'Stop'

$installFolder = (Split-Path -Parent $MyInvocation.MyCommand.Definition)
$exe = Join-Path $installFolder 'NomOrderManager.exe'
$serviceName = 'NomOrderManager'

nssm install $serviceName $exe
nssm set $serviceName AppExit Default Exit
nssm set $serviceName Description A fun web project which can be used to gather food delivery service orders
