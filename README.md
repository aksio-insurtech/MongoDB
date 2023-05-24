# Aksio MongoDB Extensions

## Packages / Deployables

[![Nuget](https://img.shields.io/nuget/v/Aksio.MongoDB?logo=nuget)](http://nuget.org/packages/aksio.mongodb)

## Builds

[![.NET Build](https://github.com/aksio-insurtech/MongoDB/actions/workflows/dotnet-build.yml/badge.svg)](https://github.com/aksio-insurtech/MongoDB/actions/workflows/dotnet-build.yml)

## Description

The Aksio MongoDB extension provides utilities to make it easier to work with MongoDB in application development.
It provides convenience methods and introduces concepts to make application development more maintainable.

## Central Package Management

This repository leverages [Central Package Management](https://learn.microsoft.com/en-us/nuget/consume-packages/Central-Package-Management), which
means that all package versions are managed from a file at the root level called [Directory.Packages.props](./Directory.Packages.props).

In addition there are also [Directory.Build.props](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory?view=vs-2022#directorybuildprops-and-directorybuildtargets) files for
setting up common settings that are applied cross cuttingly.