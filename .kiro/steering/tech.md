# Tech Stack

## Runtime & Language

- .NET 9.0
- C# 13.0 (latest)
- Target: Console application

## Project Configuration

- SDK: `Microsoft.NET.Sdk`
- Nullable reference types: enabled
- Implicit usings: enabled
- Root namespace: `LearnStructuredProgramming`

## Development Environment

- DevContainer-based (Docker + VS Code)
- Base image: `mcr.microsoft.com/dotnet/sdk:9.0`
- OS: Linux (Debian 12)

## Code Quality Tools

- EditorConfig for style enforcement
- Roslyn analyzers
- SonarLint

## Common Commands

```bash
# Build
dotnet build

# Run (shows menu to select section)
dotnet run

# Restore dependencies
dotnet restore

# Run tests
dotnet test

# Release build
dotnet publish -c Release

# Clear NuGet cache (troubleshooting)
dotnet nuget locals all --clear
```

## VS Code Extensions (DevContainer)

- C# / C# Dev Kit
- .NET Runtime
- Test Explorer
- GitLens
- EditorConfig
