@echo off
SET DIR=%~d0%~p0%
pushd %DIR%
dropkick\dk.exe execute /deployment:DeploymentSpecification\__NAME__.Deployment.dll /environment:${environment} /settings:..\environment.files\${environment} /roles:Website,VirtualDirectory
popd
pause
