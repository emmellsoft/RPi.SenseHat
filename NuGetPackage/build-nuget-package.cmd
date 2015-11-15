nuget pack "..\RPi.SenseHat\RPi.SenseHat\RPi.SenseHat.csproj" -Symbols -IncludeReferencedProjects -Prop Configuration=Release
@echo.
@echo.
@echo After a successful NuGet package build:
@echo.
@echo First execute:
@echo nuget setApiKey {API KEY}
@echo Where the {API KEY} is gotten from https://www.nuget.org/account.
@echo.
@echo Then execute:
@echo nuget push Emmellsoft.IoT.RPi.SenseHat.xxxxxxxx.nupkg
