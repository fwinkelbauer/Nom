#load "artifact.cake"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("Clean").Does(() =>
{
    CleanArtifacts();
    CleanDirectory($"NomOrderManager/bin/{configuration}");
});

Task("Restore").Does(() =>
{
    NuGetRestore("NomOrderManager.sln");
});

Task("Build").Does(() =>
{
    MSBuild("NomOrderManager.sln", new MSBuildSettings { Configuration = configuration, WarningsAsError = true });
    StoreBuildArtifacts("NomOrderManager", $"NomOrderManager/bin/{configuration}/**/*");
});

Task("CreatePackages").Does(() =>
{
    PackChocolateyArtifacts("Chocolatey/**/*.nuspec");
});

Task("Default")
    .IsDependentOn("Clean")
    .IsDependentOn("Restore")
    .IsDependentOn("Build")
    .IsDependentOn("CreatePackages");

RunTarget(target);
