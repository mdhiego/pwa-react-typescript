# Dotnet tools used in the repo

## dotnet-format
### Overview
The dotnet-format tool is used to format the code to match the .editorconfig settings for this repo.

---
## dotnet-ef
### Overview
dotnet-ef is a tool for managing Entity Framework Core migrations and updating databases from a .NET CLI.
### Usage
#### Add a migration
```dotnet ef migrations add <migration name>```
#### Add a migration to Default Detentora DbContext
```dotnet ef --startup-project ..\..\..\..\apps\teros-banking\src\WebHost\ migrations add InitialCreate --context DetentoraDbContext -o .\Infrastructure\Persistence\Migrations```
#### Update the database
```dotnet ef database update```
#### Update the database of the Default Detentora DbContext
```dotnet ef --startup-project ..\..\..\..\apps\teros-banking\src\WebHost\ database update --context DetentoraDbContext```

---
## dotnet-stryker
### Overview
Stryker is a mutation testing framework for .NET. It will instrument your code, run your tests and report which parts of your code were effectively 'killed' by your tests. The code that was not killed is code that is not covered by your tests and is therefore 'susceptible' to bugs.

---
## snitch
### Overview
A tool that help you find transitive package references that can be removed.

---
## dotnet-outdated-tool

### Overview
A .NET tool that allows you to quickly report on any outdated NuGet packages in your .NET Core and .NET Standard projects.
### Usage
#### Dotnet minor version list
```dotnet dotnet-outdated -vl Minor```
#### Dotnet minor version auto update
```dotnet dotnet-outdated -vl Minor -u```
