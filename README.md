# ASP.NET Core Web API Test Assignment

Welcome to the ASP.NET Core Web API test assignment. This task involves developing a Web API that supports CRUD operations for managing a user list.

## Requirements

1. **Data Model:**
    - Define a `User` model with the following attributes:
        - `Id`
        - `Name`
        - `Age`
        - `Email`
        - Associated entity `Role` with possible values of User, Admin, Support, and SuperAdmin
        - A `User` can have multiple roles.

2. **Features**
    - Implement a `UsersController` with methods to perform the following operations:
      - Retrieve a list of all users with pagination, sorting, and filtering based on each attribute in the `User` model.
      - Retrieve a user by `Id` along with all their roles.
      - Add a new role to a user.
      - Create a new user.
      - Update user information by `Id`.
      - Delete a user by `Id`.

3. **Rules:**
   - Add data validation logic:
     - Ensure required fields (`Name`, `Age`, `Email`) are present.
     - Verify the uniqueness of `Email`.
     - Check that `Age` is a positive number.
     - Add authentication and authorization to your API using JWT tokens.

4. **Data Access:**
    - Use Entity Framework Core (or another ORM of your choice) for data access and to store users and roles in a database.
    - Create a migration to generate the necessary database tables.


## Additional Tasks (Optional)

- Configure logging for API actions (e.g., using Serilog).
- Docker support.