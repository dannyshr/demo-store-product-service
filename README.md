📦 demo-store-product-service
This repository hosts the .NET Core API for the Demo Store, responsible for managing product categories 
and individual product data. It serves as a backend for the demo-store-frontend application.
Url: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/swagger/index.html

✨ Features
Product Management: CRUD operations for products (if implemented).

Category Management: Provides endpoints for product categories.

Database Integration: Connects to an Azure SQL Database for data persistence.

Swagger/OpenAPI Documentation: Self-documenting API endpoints.

🚀 Getting Started (Local Development)
To get this API running on your local machine, ensure you have the following:

Prerequisites
.NET SDK (v8.0 or later):

SQL Server: A local SQL Server instance or access to an Azure SQL Database.

Git: Download & Install Git

Installation
Clone the repository:

git clone https://github.com/your-username/demo-store-product-service.git
cd demo-store-product-service

(Replace your-username with your actual GitHub username)

Restore NuGet packages:

dotnet restore

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
  "ProductServiceDatabaseSettings": {
    "Server": "localhost", // Or your local SQL Server instance
    "Database": "DemoStoreProductsDB",
    "UserId": "yourLocalDbUser",
    "Password": "yourLocalDbPassword",
    "OtherParams": "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "CorsSettings": {
    "AllowedOrigins": [
      "http://localhost:3001" // Or your local frontend URL
    ]
  }
}

Note: The ProductServiceDatabaseSettings will map to your ProductServiceDatabaseSettings.cs class.

Running Locally
To run the API locally:

dotnet run

The API will typically be available at https://localhost:5001 (or http://localhost:5000).

API Endpoints (Local)
Base URL: https://localhost:5001 (or http://localhost:5000)

Categories: https://localhost:5001/api/Categories

Products: https://localhost:5001/api/Products

Swagger UI: https://localhost:5001/swagger

☁️ Deployment (CI/CD)
This application is deployed to Azure App Service (Web App) using a GitHub Actions pipeline.

Azure Services Used
Azure App Service: Hosts the .NET API.

Azure SQL Database: Provides the backend database.

CI/CD Pipeline Overview
The deployment pipeline is defined in .github/workflows/main_demostoreproductapi-YOUR_NAME.yml.

Trigger: The pipeline automatically triggers on pushes to the main branch.

Build: The .NET project is built and published.

Deploy: The azure/webapps-deploy@v2 action deploys the compiled application to the Azure App Service.

Environment Variables in Azure
The following environment variables are configured as Application Settings in your Azure App Service resource in the Azure Portal (under Configuration > Application settings). These override the appsettings.json values for the deployed environment.

ProductServiceDatabaseSettings__Server: tcp:your-azure-sql-server.database.windows.net,1433

ProductServiceDatabaseSettings__Database: DemoStoreProductsDB

ProductServiceDatabaseSettings__UserId: Your Azure SQL DB username

ProductServiceDatabaseSettings__Password: Your Azure SQL DB password

ProductServiceDatabaseSettings__OtherParams: MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;

CorsSettings__AllowedOrigins__0: https://demostorefrontend-yourname.azurewebsites.net (Your deployed frontend URL)

CorsSettings__AllowedOrigins__1: (Any other allowed origins, e.g., for NestJS Order Service or Postman)

🌐 Deployed API Endpoints
Base URL: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net

Categories: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/api/Categories

Products: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/api/Products

Swagger UI: https://demostoreproductapi-dannys-gkg9eehhgjhaaab5.israelcentral-01.azurewebsites.net/swagger

Note: Swagger UI is explicitly enabled for production in Program.cs.

Controllers can be excluded from Swagger using DocInclusionPredicate in Program.cs.

📂 Project Structure
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

📄 License
Public