# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This repository demonstrates protecting personal and sensitive data using Microsoft's compliance redaction library (`Microsoft.Extensions.Compliance.Redaction`). It contains two example projects:

- **Example01**: Console application demonstrating redaction
- **Example02**: Web API application with Swagger demonstrating redaction

## Build and Run Commands

### Build the solution
```bash
dotnet build
```

### Run Example01 (Console App)
```bash
dotnet run --project src/Example01/Example01.csproj
```

### Run Example02 (Web API)
```bash
dotnet run --project src/Example02/Example02.csproj
```
Access Swagger UI at: https://localhost:7285/swagger (port may vary)

### Restore dependencies
```bash
dotnet restore
```

### Run tests
```bash
dotnet test
```

## Technology Stack

- .NET 9.0 (as specified in global.json and Directory.Build.props)
- Central Package Management enabled (Directory.Packages.props)
- Code analysis enabled with warnings treated as errors
- Key packages:
  - Microsoft.Extensions.Compliance.Redaction
  - Microsoft.Extensions.Telemetry
  - Microsoft.Extensions.Hosting (console app)
  - ASP.NET Core with Swagger (web API)

## Architecture and Redaction System

### Data Classification Taxonomy

The codebase defines a custom data taxonomy in `Features/Redaction/RedactionAttributes.cs`:
- **Sensitive**: For credentials and sensitive data (e.g., Login, Password)
- **Personal**: For personally identifiable information (e.g., FirstName, LastName)

Both examples use the same taxonomy definition with `DataClassification` and custom attributes:
```csharp
public sealed class SensitiveAttribute() : DataClassificationAttribute(DataTaxonomy.Sensitive);
public sealed class PersonalAttribute() : DataClassificationAttribute(DataTaxonomy.Personal);
```

### Redaction Strategy

Configured in `Features/Redaction/RedactionConfiguration.cs`:
- **Sensitive data**: Uses `StarRedactor` (custom redactor that replaces characters with asterisks)
- **Personal data**: Uses HMAC redactor (cryptographic hash for consistent pseudonymization)

### Logging Integration

The redaction system integrates with Microsoft.Extensions.Logging through:
1. `LoggingExtensions.AddLogging()`: Sets up JSON console logging with redaction enabled
2. `UserExtensions.LogUserRetrieved()`: Uses `[LogProperties]` attribute with source generators to automatically log User properties
3. Properties decorated with redaction attributes (`[Sensitive]`, `[Personal]`) are automatically redacted in logs
4. Properties with `[LogPropertyIgnore]` (e.g., FullName computed property) are excluded from logging

### DependencyInjection Pattern

Both examples follow the same extension method pattern:
- Console app: `IHostBuilder.AddServices()` extension
- Web API: `WebApplicationBuilder.AddServices()` extension

These methods configure:
- Logging with redaction
- Service registration (e.g., `IUserService`)

### Code Organization

Both Example01 and Example02 share the same feature-based folder structure:
- `Features/Users/`: User domain (User model, UserService, UserExtensions for logging)
- `Features/Redaction/`: Redaction infrastructure (attributes, configuration, custom redactors)
- `Extensions/`: Cross-cutting concerns (LoggingExtensions, SwaggerExtensions in Example02)
- `DependencyInjection.cs`: Service registration
- `Program.cs`: Application entry point

The code is duplicated between examples to keep each self-contained for demonstration purposes.
