# PaginationServices

## Overview
PaginationServices is a .NET 6 service designed to interface with a Microsoft SQL Server database to demonstrate pagination capabilities.

## Prerequisites

- Microsoft SQL Server
- .NET 6 SDK and runtime

## Setup Instructions

### 1. Database Configuration
Ensure you have a SQL Server database available and you can connect to it successfully. Update the connection string in the application settings with your server details.

### 2. Package Installation
The project relies on several NuGet packages. Use the following commands to install them:

```powershell
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.VisualStudio.Web.CodeGeneration.Design
Install-Package Microsoft.EntityFrameworkCore.Design
Install-Package Microsoft.EntityFrameworkCore.Tools


### 3. Database Creation
Run the provided SQL script to create the Inventory database and the Items table you find the script (Migrations/DataBaseScripts).
### 4. Entity Framework Core Scaffold
Scaffold-DbContext "Server=yourServerName;Database=Inventory;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
Remember to replace yourServerName with the actual name of your server.
### 5. Add-Migration InitialCreate


Featuers
### 1.Pagination 
### 2.Filtering
### 3.Searching
### 4.Sorting
