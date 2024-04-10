### IdentityAuthService

IdentityAuthService is a .NET 8 Web API Application that utilizes MSSQL Server as its underlying database.

#### Setup Instructions

To configure IdentityAuthService for use with `IdentityUser`, follow these steps:

1. Install the required NuGet packages:
   - IdentityAuthService.DbContext
   - IdentityAuthService.Services
   - Microsoft.AspNetCore.Identity
   - Microsoft.EntityFrameworkCore

2. Uncomment the following code snippet in `Startup.cs` to add the `IdentityUser` service to the project:

```
services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetValue<string>("ConnectionStrings:DefaultConnection")));

services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
```

3. Then utilize `Microsoft.AspNetCore.Identity.UserManager` to retrieve data from the identity database.
4. DbContext is also provided for managing `IdentityUser`.

#### User.cs

- Consists of the following properties. These fields will be usable for other operations. The password will not be stored in the database.
    `string UserFirstName`
    `string UserLastName`
    `string UserEmailAddress`
    `string UserPhoneNumber`
    `string UserAddress`
    `bool IsMigrated`

The `isMigrated` field tracks whether the user has been migrated from the database or not.

#### AuthController.cs

The `AuthController` includes login and signup actions, both annotated with `AllowAnonymous` and `HttpPost`.

- **Login:** Expects `string SignInName` and `string Password` parameters to validate the user login process.
- **SignUp:** Expects `string SignInName`, `string FirstName`, `string LastName`, and `string PhoneNumber` parameters. The password is stored in Azure AD B2C only.  Essential user data is added to the custom database for other operations.

#### AuthService.cs

The `AuthService` manages essential user data for other operations. Since users are migrated, updates to the Identity User table are unnecessary.

- **UserLoginValidation:** Checks user availability in the database. If the user is available but not marked as migrated, it updates the user as migrated and returns `userDetails`, `roles`, and `needToMigrate` (set to 'local') fields with a 200 OK status. If the user is available but already marked as migrated, it returns `roles` and `needToMigrate` (set to null) fields with a 200 OK status.
- **UserSignUpValidation:** Checks user availability in the database. If the user is available, it returns `true`. Otherwise, it adds essential user details to the User database and returns `false`.
