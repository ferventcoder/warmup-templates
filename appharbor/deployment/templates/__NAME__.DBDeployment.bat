@echo off

SET DIR=%~d0%~p0%

SET sql.files.directory="%DIR%${dirs.db}\db"
SET connection.string="${connection.string}"
SET repository.path="${repository.path}"
SET version.file="${file.version}"
SET version.xpath="//buildInfo/version"
SET environment=${environment}

"%DIR%roundhouse\rh.exe" /cs=%connection.string% /f=%sql.files.directory% /vf=%version.file% /vx=%version.xpath% /r=%repository.path% /env=%environment% /simple

pause