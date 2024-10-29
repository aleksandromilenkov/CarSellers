# Car Management API

## Overview

This is an ASP.NET Web API project built with .NET 6.0, designed for managing a car marketplace. The API provides various functionalities for users and admins, including user authentication, car management, and image uploads.

## Features

- **User Management**
  - Register new users
  - Login and logout functionality
  - Password reset and recovery

- **Car Management**
  - CRUD operations for users, cars, companies, car models and manufacturers
  - Admin users can edit and delete cars, companies, manufacturers and car models
  - Regular users can add or remove cars from their favorites
  - Search for cars without logging in

- **Image Upload**
  - Supports image uploads for users, cars, and companies

## Technologies Used

- **Framework:** .NET 6.0
- **Database:** Microsoft SQL Server
- **Packages:**
  - [AutoMapper](https://automapper.org/)
  - [DotNetEnv](https://github.com/mrbrun0/DotNetEnv)
  - [MailKit](https://github.com/jstedfast/MailKit)
  - [Microsoft.AspNetCore.Authentication.JwtBearer](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/jwt-bearer)
  - [Microsoft.AspNetCore.Identity.EntityFrameworkCore](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
  - [Microsoft.AspNetCore.Mvc.NewtonsoftJson](https://docs.microsoft.com/en-us/aspnet/core/mvc/json-options?view=aspnetcore-6.0#newtonsoftjson)
  - [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/)
  - [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

## Controllers

- **AccountController**: Manages user registration, login, logout, and password recovery.
- **CarController**: Handles CRUD operations for cars.
- **ManufacturerController**: Handles CRUD operations manufacturers' data.
- **CompanyController**: Handles CRUD operations for company-related data.
- **FavoriteCarsController**: Manages users' favorite cars.
- **CarModelController**: Manages car model data.

## Getting Started

### Prerequisites

- .NET SDK 6.0 or higher
- SQL Server

### Installation

1. Clone the repository:
   ```bash
   git clone https://your-repo-url.git
   cd your-repo-folder

2. Restore the packages:

```bash
dotnet restore

3. Update the database connection string in your appsettings.json file.

4. Apply the migrations:

```bash
dotnet ef database update

5. Run the application:

```bash
dotnet run

###  API Documentation
The API is documented using Swagger. You can access the documentation at:

```bash
http://localhost:5000/swagger

### Contributing
If you'd like to contribute to this project, please fork the repository and submit a pull request with your changes.

### License
This project is licensed under the MIT License.

Feel free to customize any sections as needed or add more details!
