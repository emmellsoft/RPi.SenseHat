param($installPath, $toolsPath, $package, $project)

$projectFullName = $project.FullName
$projectFileInfo = new-object -typename System.IO.FileInfo -ArgumentList $projectFullName

# Get the source paths
$sourcePath = join-path $installPath 'Content'              # E.g. "C:\Users\Mattias\.nuget\packages\Emmellsoft.IoT.RPi.SenseHat\0.1.0.7\Content"
$sourceDemosPath = join-path $installPath 'Content\Demos'   # E.g. "C:\Users\Mattias\.nuget\packages\Emmellsoft.IoT.RPi.SenseHat\0.1.0.7\Content\Demos"

# Get the dest paths
$projectPath = $projectFileInfo.DirectoryName               # E.g. "C:\Users\Mattias\Projects\Test\2015\MyNugetTest\MyNugetTest"
$projectDemosPath = join-path $projectPath 'Demos'          # E.g. "C:\Users\Mattias\Projects\Test\2015\MyNugetTest\MyNugetTest\Demos"

# Ensure the dest "Demos" path exists
if (!(test-path $projectDemosPath))
{
	md $projectDemosPath | out-null
}

Copy-Item -Path "$sourcePath\DemoRunner.cs" -Destination "$projectPath\DemoRunner.cs" -Force
Copy-Item -Path "$sourcePath\SenseHatDemo.cs" -Destination "$projectPath\SenseHatDemo.cs" -Force
Copy-Item -Path "$sourcePath\Demos\*.cs" -Destination "$projectPath\Demos\" -Force

Write-Host ""
Write-Host "Note:"
Write-Host "   ""DemoRunner.cs"" and ""SenseHatDemo.cs"" has been copied to your project folder, along with a folder called ""Demos""!"
Write-Host "    However, you will need to include them manually, since NuGet 3.1 doesn't support content source files anymore... :'("
Write-Host ""
Write-Host "    Newly copied files:"
Write-Host "    *) $projectPath\DemoRunner.cs"
Write-Host "    *) $projectPath\SenseHatDemo.cs"
Write-Host "    *) $projectPath\Demos\*.cs"
Write-Host ""
Write-Host "  Usage:"
Write-Host "  For example, simply call the DemoRunner.Run method from the constructor of the MainPage:"
Write-Host ""
Write-Host "  public MainPage()"
Write-Host "  {"
Write-Host "      this.InitializeComponent();"
Write-Host ""
Write-Host "      // Replace the ""DiscoLights"" class below with the demo you want to run! :-)"
Write-Host "      RPi.SenseHat.Demo.DemoRunner.Run(senseHat => new RPi.SenseHat.Demo.Demos.DiscoLights(senseHat));"
Write-Host "  }"


