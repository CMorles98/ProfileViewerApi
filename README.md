# ProfileViewer

ProfileViewer is a project developed using the .NET framework following Domain-Driven Design (DDD) principles.

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
  - After running the project, you will be redirect to the Swagger UI.
  - From there, you can explore and test all available endpoints.

