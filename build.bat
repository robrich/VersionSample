
REM ASSUME: nuget is in %PATH%
nuget restore
if not %errorlevel% equ 0 (
	exit /b %errorlevel%
)

PowerShell -NoProfile -NoLogo -ExecutionPolicy unrestricted -Command "(Get-Content Web\Properties\AssemblyInfo.cs).replace('GITHASH', (git rev-parse --short HEAD)) | Set-Content Web\Properties\AssemblyInfo.cs"

REM ASSUME: msbuild is in %PATH%
REM If not: set PATH=C:\Program Files (x86)\MSBuild\14.0\bin;%PATH%
msbuild VersionSample.sln /m /p:Configuration=Release /p:VisualStudioVersion=14.0
if not %errorlevel% equ 0 (
	exit /b %errorlevel%
)

REM run asp.net compile to build views
REM FRAGILE: this dies on obj folder content if you run it twice in a row
C:\Windows\Microsoft.NET\Framework\v4.0.30319\aspnet_compiler.exe -v / -p Web
if not %errorlevel% equ 0 (
	exit /b %errorlevel%
)

REM run publish
msbuild Web\Web.csproj /m /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=Web /p:VisualStudioVersion=14.0
