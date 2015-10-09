nuget pack "..\RPi.SenseHat\RPi.SenseHat\RPi.SenseHat.csproj" -Symbols -IncludeReferencedProjects -Prop Configuration=Release
@echo.
@echo.
@echo After a successful NuGet package build, execute:
@echo nuget push Emmellsoft.IoT.RPi.SenseHat.xxxxxxxx.nupkg