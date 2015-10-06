nuget pack "..\RPi.SenseHat\Rpi.SenseHat\Rpi.SenseHat.csproj" -Symbols -IncludeReferencedProjects -Prop Configuration=Release
@echo.
@echo.
@echo After a successful NuGet package build, execute:
@echo nuget push Emmellsoft.IoT.RPi.SenseHat.xxxxxxxx.nupkg