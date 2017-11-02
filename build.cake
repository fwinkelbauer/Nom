#load "nuget:?package=cake.mug.tools"
#load "nuget:?package=cake.mug"

var target = Argument("target", "Default");
BuildParameters.Configuration = Argument("configuration", "Release");
BuildParameters.ArtifactsDir = "./BuildArtifacts";

PackageParameters.ChocolateySpecs.Add("Chocolatey/NomOrderManager.nuspec");

Task("Default")
    .IsDependentOn("Analyze")
    .IsDependentOn("CreatePackages")
    .Does(() =>
{
});

RunTarget(target);
