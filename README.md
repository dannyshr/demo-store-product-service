# 📦 demo-store-product-service
This repository hosts the .NET Core API for the Demo Store, responsible for managing product categories and individual product data. It serves as a backend for the demo-store-frontend application.

Url: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/swagger/index.html

# ✨ Features
Category Management: Provides endpoints for category CRUD operations.

Product Management: CRUD operations for products.

Database Integration: Connects to an Azure SQL Database for data persistence.

Swagger/OpenAPI Documentation: Self-documenting API endpoints.

# 🚀 Getting Started (Local Development)
To get this API running on your local machine, ensure you have the following:

## Prerequisites
.NET SDK (v8.0 or later):

SQL Server: A local SQL Server instance or access to an Azure SQL Database.

Git: Download & Install Git

## Installation
Clone the repository:

git clone https://github.com/dannyshr/demo-store-product-service.git

cd demo-store-product-service

Restore NuGet packages: dotnet restore

Configure Database Connection for Local Development:
Update your appsettings.Development.json file with your local SQL database connection details.

// appsettings.Development.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "DatabaseSettingsConfig": {
    "Server": "localhost", // your local SQL Server instance
    "Database": "DemoStoreProductsDB",
    "UserId": "yourLocalDbUser",
    "Password": "yourLocalDbPassword",
    "OtherParams": "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "CorsSettingsConfig": {
    "AllowedOrigins": [
      "http://localhost:3001" // your local frontend URL
    ]
  }
}

Note: The DatabaseSettingsConfig will map to the DatabaseSettingsConfig.cs class.

## Running Locally
To run the API locally:

dotnet run

The API will typically be available at https://localhost:5001 (or http://localhost:5000).

API Endpoints (Local)
Base URL: https://localhost:5001 (or http://localhost:5000)

Categories: https://localhost:5001/api/Categories

Products: https://localhost:5001/api/Products

Swagger UI: https://localhost:5001/swagger

# ☁️ Deployment (CI/CD)
This application is deployed to Azure App Service (Web App) using a GitHub Actions pipeline.

Azure Services Used: Azure App Service which hosts the .NET API.

Azure SQL Database: Provides the backend database.

CI/CD Pipeline Overview
The deployment pipeline is defined in .github/workflows/main_demostoreproductapi-dannys.yml.

Trigger: The pipeline automatically triggers on pushes to the main branch.

Build: The .NET project is built and published.

Deploy: The azure/webapps-deploy@v2 action deploys the compiled application to the Azure App Service.

### Environment Variables in Azure
The following environment variables are configured as environment variables through the Settings menu in your Azure App Service resource in the Azure Portal (under Settings > Environment variables). These override the appsettings.json values for the deployed environment.

DatabaseSettingsConfig__Server: tcp:your-azure-sql-server.database.windows.net,1433

DatabaseSettingsConfig__Database: DemoStoreProductsDB

DatabaseSettingsConfig__UserId: Your Azure SQL DB username

DatabaseSettingsConfig__Password: Your Azure SQL DB password

DatabaseSettingsConfig__OtherParams: MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

CorsSettingsConfig__AllowedOrigins__0: (Your deployed frontend URL)

## 🌐 Deployed API Endpoints
Base URL: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net

Categories: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/api/Categories

Products: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/api/Products

Swagger UI: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/swagger

Note: Swagger UI is explicitly enabled for production in Program.cs.

# 📂 Project Structure
.

├── Controllers/           # API Controllers

├── Models/                # Data Models

├── appsettings.json

├── appsettings.Development.json

├── Program.cs             # Application entry point and configuration

├── .github/

│   └── workflows/         # GitHub Actions workflow file

├── demo-store-product-service.csproj

└── README.md

# 📄 License
Public