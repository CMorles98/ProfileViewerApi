# ProfileViewer

ProfileViewer is a .NET project built with a focus on clean architecture and design patterns, leveraging validator, repository, and manager patterns to ensure robustness, maintainability, and extensibility.

## Overview

ProfileViewer provides a platform to view user profiles, implemented using Domain-Driven Design (DDD) principles. It employs the following patterns and technologies:

- **Validator Pattern**: Input validation is enforced using a validator pattern, ensuring that data integrity is maintained throughout the application.
  
- **Repository Pattern**: Data access is abstracted through the repository pattern, allowing for separation of concerns and facilitating testability and scalability.
  
- **Manager Pattern with Lazy Loading**: The manager pattern is utilized for handling business logic, with lazy loading incorporated for repository injection, promoting efficiency and minimizing resource consumption.

## Technologies Used

- **Language**: C#
  
- **Database**: SQL Server
  
- **Framework**: .NET
  
- **Patterns**: Validator, Repository, Manager (with Lazy Loading)

## Additional Features

In addition to the core functionality, the API incorporates the following technologies and features:

- **Cache**: Utilizes caching mechanisms to optimize performance and reduce database load.
  
- **Versioning**: Supports API versioning to ensure backward compatibility and smooth transitions between different versions.
  
- **Global Exception Handling**: Implements centralized exception handling to provide consistent error responses across the application.
  
- **Logging**: Utilizes logging mechanisms to track and analyze application behavior and diagnose issues.
  
- **AutoMapper**: Simplifies object-to-object mapping to streamline data transformations between layers.
  
- **Paging**: Implements pagination for large datasets to enhance performance and user experience.
  
- **JWT (Json Web Tokens)**: Implements JWT for secure authentication and authorization, providing stateless authentication tokens.

- **ORM**: Utilizes Entity Framework Core as the ORM framework for efficient data access and manipulation.

## Dockerization

The API has been Dockerized, allowing for easy deployment and scalability using containerization technology.

## Requirements

- .NET SDK 8 installed
- Local or remote database server

## Getting Started

1. Clone the repository:

    ```bash
    git clone https://github.com/CMorles98/ProfileViewer.git
    ```

2. Navigate to the project directory:

    ```bash
    cd Backend
    ```

3. Open the solution file `ProfileViewer.sln` in Visual Studio or your preferred IDE.

4. Configure the Database Connection:
   
   - If using a local database server:
     - Open the `appsettings.json` file.
     - Modify the `ConnectionStrings` section to point to your local database server.

   - If using a remote database server:
     - Ensure your server is accessible.
     - Open the `appsettings.json` file.
     - Modify the `ConnectionStrings` section to point to your remote database server.

5. Run Initial Migration:

   - Open Package Manager Console in Visual Studio (`Tools > NuGet Package Manager > Package Manager Console`).
   - Run the following command to apply the initial migration and create the database schema:
   
     ```bash
     update-database
     ```

6. Run the Project:

   - Press F5 in Visual Studio.

## Additional Notes

- Users can test any endpoint using Swagger:
  - After running the project, you will be redirected to the Swagger UI.
  - From there, you can explore and test all available endpoints.

