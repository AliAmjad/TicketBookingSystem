
## Technologies

* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/)
* [Angular 14](https://angular.io/)
* [MediatR](https://github.com/jbogard/MediatR)
* [AutoMapper](https://automapper.org/)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) & [Respawn](https://github.com/jbogard/Respawn)

## Getting Started

1. Install the latest [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
2. Install the latest [Node.js LTS](https://nodejs.org/en/)
3. Navigate to `src/WebUI` and launch the project using `dotnet run`
4. OR in Visual studio just press F5 to run the solution.

### Database Configuration

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

## Overview

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### WebUI

This layer is a single page application based on Angular 14 and ASP.NET Core 7. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

## Demo

https://www.loom.com/share/77b5cd4fadac4bbab6fb479dcf08272c

## Default credentials seeded

1. Admin: administrator@localhost/Administrator1!
2. User: jason@localhost/User1!