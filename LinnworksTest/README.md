# LinnworksTest — SDET Test Automation

Test automation suite for the Linnworks category management web application. Includes unit tests, integration (database) tests, and UI (Selenium) tests.

## Tech Stack & Prerequisites

| Component           | Technology                              |
|---------------------|-----------------------------------------|
| Language            | C# (.NET Core 2.1 / 2.2)               |
| Web framework       | ASP.NET Core 2.1 + Angular CLI          |
| Database            | SQL Server (EF Core + Dapper)           |
| Unit/integration    | NUnit 3 + FluentAssertions + Moq        |
| UI tests (NUnit)    | NUnit 3 + Selenium WebDriver (Firefox)  |
| UI tests (xUnit)    | xUnit 2.4 + Selenium WebDriver (Firefox)|
| Build system        | dotnet CLI / MSBuild                    |

### Prerequisites

- [.NET Core SDK 2.2](https://dotnet.microsoft.com/download/dotnet/2.2) (or later 2.x)
- [Node.js](https://nodejs.org/) (for the Angular SPA frontend)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server)
- [Firefox](https://www.mozilla.org/firefox/) + [geckodriver](https://github.com/mozilla/geckodriver/releases) (for Selenium UI tests)

## Getting Started

### 1. Restore the database

Restore the backup file `Linnworks.TestDb.bak` into your SQL Server instance:

```sql
RESTORE DATABASE [Linnworks.TestDb]
FROM DISK = 'C:\path\to\Linnworks.TestDb.bak'
WITH MOVE 'Linnworks.TestDb' TO 'C:\...\Linnworks.TestDb.mdf',
     MOVE 'Linnworks.TestDb_log' TO 'C:\...\Linnworks.TestDb_log.ldf';
```

### 2. Configure connection strings

Edit `LinnworksTest/appsettings.Development.json` to match your SQL Server instance:

```json
{
  "ConnectionStrings": {
    "LinnworksDatabase": "Server=YOUR_SERVER;Database=Linnworks.TestDb;Integrated Security=True"
  }
}
```

The `DatabaseFactory` project also reads from `appsettings.json` using the key `LinnworksDatabase`.

### 3. Install dependencies & run the app

```bash
cd LinnworksTest
dotnet restore
cd LinnworksTest
npm install --prefix ClientApp
dotnet run
```

The app runs at `http://localhost:59509/` by default. Use token `bccf905c-6592-40f2-8db1-c976791fa40a` to log in (must exist in the `Tokens` table).

## Running Tests

### All tests

```bash
dotnet test LinnworksTest.sln
```

### Unit & integration tests only (NUnit)

```bash
dotnet test LinnWorksNUnitTestProject/LinnWorksNUnitTestProject.csproj
```

### UI tests — NUnit (requires running app + Firefox + geckodriver)

```bash
dotnet test LinnWorksUITests/LinnWorksUITests.csproj
```

### UI tests — xUnit (requires running app + Firefox + geckodriver)

```bash
dotnet test LinnWorksXUnitTestProject/LinnWorksXUnitTestProject.csproj
```

### Run a single test

```bash
dotnet test --filter "FullyQualifiedName~Login_WhenTokenIsNotPresentInDb_ReturnsBadRequest"
```

## Environment Variables

UI tests read these environment variables (with fallback defaults):

| Variable                | Default                                    | Description               |
|-------------------------|--------------------------------------------|---------------------------|
| `LINNWORKS_SERVICE_URL` | `http://localhost:59509/`                  | Base URL of the web app   |
| `LINNWORKS_TEST_TOKEN`  | `bccf905c-6592-40f2-8db1-c976791fa40a`     | Auth token for UI tests   |

## Folder Structure

```
LinnworksTest/
├── LinnworksTest/                  # ASP.NET Core web app (API + Angular SPA)
│   ├── ClientApp/                  # Angular frontend
│   ├── Controllers/                # API controllers (Auth, Category)
│   ├── DataAccess/                 # EF Core DbContext, repositories, entities
│   ├── Models/                     # API view models / DTOs
│   ├── Migrations/                 # EF Core migrations
│   └── Pages/                      # Razor error page
├── DatabaseFactory/                # Database helper library (Dapper)
│   ├── DbCRUD/                     # Test CRUD helpers
│   └── Models/                     # DB models for test helpers
├── LinnWorksNUnitTestProject/      # NUnit tests
│   ├── ControllerTests/            # Unit tests (mocked controllers)
│   ├── IntegrationDBTests/         # Integration tests (real database)
│   └── _dbtest/                    # DB connection helper
├── LinnWorksUITests/               # NUnit Selenium UI tests
│   ├── Base/                       # WebDriver setup, constants
│   ├── Pages/                      # Page Object Model classes
│   └── Tests/                      # UI test classes
├── LinnWorksXUnitTestProject/      # xUnit Selenium UI tests
├── Linnworks.TestDb.bak            # SQL Server database backup
├── .editorconfig                   # Code formatting rules
├── .gitignore                      # Git ignore rules
└── LinnworksTest.sln               # Visual Studio solution file
```

## Naming Conventions

### Projects & Namespaces
- **Solution/project names**: PascalCase (e.g., `LinnworksTest`, `DatabaseFactory`)
- **Namespaces**: Match folder structure, PascalCase (e.g., `LinnworksTest.DataAccess`)

### Folders
- PascalCase for all folders (e.g., `Controllers/`, `DataAccess/`, `Pages/`)

### Classes & Files
- **File names** match class names exactly, PascalCase (e.g., `CategoryController.cs`)
- **Entity classes**: Singular noun (e.g., `Category`, `Product`, `Token`)
- **Repository classes**: `{Entity}Repository` (e.g., `TokenRepository`)
- **Interfaces**: `I{Name}` prefix (e.g., `IGenericRepository`, `ITokenRepository`)

### Test Classes
- **Test class names**: `{Feature}Tests` or `{Feature}Test` (e.g., `AuthControllerTest`, `CategoryTest`)
- **Page test classes**: `{Page}PageTest` (e.g., `LoginPageTest`)

### Test Methods
- **Pattern**: `MethodUnderTest_Scenario_ExpectedResult`
- All segments PascalCase (e.g., `Login_WhenTokenIsNotPresentInDb_ReturnsBadRequest`)
- Use Arrange / Act / Assert comments

### Page Objects
- Named after the page/component they represent (e.g., `Login`, `Categories`, `AddCategory`)
- Inherit from `Base` page class

### Constants & Configuration
- Constants: PascalCase (e.g., `ServiceUrl`, `CategoryName`)
- Connection string keys: PascalCase (e.g., `LinnworksDatabase`)
- Environment variables: UPPER_SNAKE_CASE (e.g., `LINNWORKS_SERVICE_URL`)

## Known Issues

| Issue | File(s) | Notes |
|-------|---------|-------|
| .NET Core 2.1/2.2 is end-of-life | All `.csproj` files | Should upgrade to .NET 6+ |
| NUnit/xUnit/Selenium packages are outdated | All test `.csproj` files | Run `dotnet list package --outdated` |
| EF Core 3.1.2 used in netcoreapp2.2 projects | `LinnWorksNUnitTestProject.csproj`, `LinnWorksUITests.csproj` | Version mismatch — EF Core 3.x requires netcoreapp3.0+ |
| `_dbtest` folder uses non-standard naming | `LinnWorksNUnitTestProject/_dbtest/` | Should be renamed to `DbTest` (PascalCase) — not renamed to avoid breaking project references |
| Namespace `_dbtest` is non-conventional | `_dbtest/openConnection.cs` | Should match folder rename |
| `Categories` model uses plural name | `DatabaseFactory/Models/Categories.cs` | Should be singular `Category` — not renamed to avoid breaking Dapper mappings |
| `CategoryTestPage` has unused constructors | `LinnWorksUITests/Tests/CategoryTestPage.cs` | Has 3 constructors; only the parameterless one is used |
| `Linnworks.TestDb.bak` committed to repo | Root directory | Large binary file; consider storing externally or in releases |
| No CI pipeline configured | — | Consider adding GitHub Actions or Azure DevOps pipeline |
| Duplicate test for bad-request login | `LinnWorksNUnitTestProject/ControllerTests/AuthControllerTest.cs` | `Login_WhenTokenIsNotPresentInDb_ReturnsBadRequest` and `Login_ReturnBadRequest_WhenAccount_DoesNotExist` test the same scenario |

## Troubleshooting

### "Connection string not found" or "LinnworksDatabase" key missing
Ensure both `appsettings.json` and `appsettings.Development.json` use the key `LinnworksDatabase` (not `LinnworksDB`).

### Selenium tests fail to start Firefox
- Ensure Firefox is installed
- Ensure `geckodriver` is on your `PATH` or in the test output directory
- Set `LINNWORKS_SERVICE_URL` if the app runs on a different port

### EF Core version mismatch warnings
The test projects target `netcoreapp2.2` but reference EF Core 3.1.2. This may produce warnings or runtime errors. To fix, either downgrade EF Core to 2.x or upgrade the projects to `netcoreapp3.1+`.

### npm install fails in ClientApp
Ensure Node.js is installed and accessible. The Angular frontend uses Angular CLI 1.x — if you have a newer global Angular CLI, you may need to use `npx` or install the correct version.

### Tests require a running SQL Server
Integration tests connect directly to a SQL Server database. Ensure:
1. SQL Server is running
2. The `Linnworks.TestDb` database is restored
3. Connection strings are correct in `appsettings.json`
