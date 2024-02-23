# Identity Server with EfCore.

* Creating an inentity provider using an Duende Identity Server. 
* Implementation of OpenIdConnect to configure single-sign-on functionality in our application.

## To setup
* Create web api for Identity provider.
* Execute 'dotnet new isempty' command to configure an empty duende identity server into your project.
* Install necessary packages for duende identity server and entity framework core.
* Add Configuration and Operational Stores and configure IdentityServer in Program.cs
* Execute these two commands to create migrations:
  - dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
  - dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb
* Add InitializeDatabase() method in HostingExtensions.cs and ConfiugrePipeLine() method.
* Run the project and it will migrate all in-memory data to database.
