# WeatherForecastApp API

A .NET Clean Architecture Web API with **CQRS** (MediatR), **Unit of Work**, **FluentValidation**, **Result pattern**, and **Specification pattern**. Uses PostgreSQL and Entity Framework Core.

This solution was created from the **Clean Architecture API** template.

---

## Solution structure

| Folder | Project | Description |
|--------|---------|-------------|
| **src/** | WeatherForecastApp.API | ASP.NET Core Web API, controllers, middleware |
| **src/** | WeatherForecastApp.Application | Use cases, CQRS, DTOs, contracts, services, specifications |
| **src/** | WeatherForecastApp.Domain | Entities, value objects, exceptions (no dependencies) |
| **src/** | WeatherForecastApp.Infrastructure | EF Core, PostgreSQL, repositories, Unit of Work, seed |
| **tests/** | WeatherForecastApp.UnitTests | xUnit unit tests |

---

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL (local or remote)

---

## Getting started

### 1. Connection string

Edit `src/WeatherForecastApp.API/appsettings.Development.json` and set your PostgreSQL connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=WeatherForecastAppDb;Username=postgres;Password=YOUR_PASSWORD"
}
```

### 2. Run migrations

Install the EF Core tool (if needed):

```bash
dotnet tool install --global dotnet-ef
```

Apply migrations:

```bash
dotnet ef database update --project src/WeatherForecastApp.Infrastructure --startup-project src/WeatherForecastApp.API
```

### 3. Run the API

```bash
dotnet run --project src/WeatherForecastApp.API
```

In Development, open **https://localhost:7043/scalar/v1** (or the URL shown in the console) for the API docs.

### 4. Sample endpoint

```http
POST /api/Sample
Content-Type: application/json

{"name": "Hello"}
```

---

## Run tests

```bash
dotnet test tests/WeatherForecastApp.UnitTests/WeatherForecastApp.UnitTests.csproj
```

---

## Add a new feature

1. **Domain** – Add entity in `src/WeatherForecastApp.Domain/Entities`, value objects in `ValueObjects` if needed.
2. **Application** – Add DTOs under `DTOs/YourFeature`, contract in `Contracts/Repositories`, command/query and handler under `Features/YourFeature`, optional specification in `Specifications`.
3. **Infrastructure** – Add EF configuration in `Persistence/Configurations`, repository implementation, register in `Extensions/ServiceCollectionExtensions.cs`.
4. **API** – Add controller and register any new services.

Only **Unit of Work** should call `SaveAsync()`; handlers and services use repositories via UoW and then call `unitOfWork.SaveAsync()` (often inside a transaction: `BeginTransactionAsync` → … → `SaveAsync` → `CommitTransactionAsync`).
