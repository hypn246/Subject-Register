## Setup
To run project, please
- Delete folder \ExtensionDKM\Migrations (if there is any)

- Clear the build cache by running this in terminal:
```bash
dotnet clean
```
- Connect to your sql server then get connection string and change it in `appsettings.json`->`DefaultConnectionString`

- Migrate db by run command in Package Manage Console/Powershell:
```bash
Add-Migration “Init”
Update-Database
```
- Default user (need seeding user for func test): username=admin & password=123