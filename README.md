# 🔐 AuthApp - Authentication & Authorization System

A simple authentication and authorization system built with ASP.NET Core using JWT Tokens and Refresh Tokens.

## 📋 Overview

This project is a Web API application built on ASP.NET Core 9.0 that provides a complete authentication system with:
- User registration
- User login
- JWT Token issuance
- Refresh Token system for renewing Access Tokens
- Role-based authorization system - Admin, SuperAdmin, User

## ✨ Features

- ✅ **JWT Authentication** - Authentication using JWT Tokens
- ✅ **Refresh Tokens** - Automatic Access Token renewal
- ✅ **Role-Based Authorization** - Multi-level permission system
- ✅ **Password Hashing** - Password encryption using ASP.NET Identity PasswordHasher
- ✅ **Entity Framework Core** - Using EF Core with SQL Server
- ✅ **Swagger/OpenAPI** - Automatic API documentation

## 🛠️ Technologies Used

- **.NET 9.0** - Core framework
- **ASP.NET Core Web API** - API framework
- **Entity Framework Core 9.0** - ORM for database operations
- **SQL Server** - Database
- **JWT Bearer Authentication** - Authentication system
- **Swagger/OpenAPI** - API documentation

## 📦 Prerequisites

Before running the project, make sure you have installed:

1. **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
2. **SQL Server** - SQL Server Express, LocalDB, or any SQL Server version
3. **Visual Studio 2022** or **Visual Studio Code** (optional)

## 🚀 How to Run

### 1. Clone the Project

```bash
git clone <repository-url>
cd AuthApp
```

### 2. Database Setup

Make sure SQL Server is running on your machine, then modify the `ConnectionString` in `appsettings.json` if needed:

```json
"ConnectionStrings": {
    "UserDatabase": "Server=.;Database=UserDb;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

### 3. Create Database (Migrations)

Open Terminal in the project folder and run:

```bash
cd AuthApp
dotnet ef database update
```

If you don't have `dotnet ef` installed, install it first:

```bash
dotnet tool install --global dotnet-ef
```

### 4. Run the Project

```bash
dotnet run
```

Or from Visual Studio:
- Press `F5` or `Ctrl+F5`

### 5. Access Swagger UI

After running the project, open your browser at:

```
https://localhost:5001/swagger
```

or

```
http://localhost:5000/swagger
```

(The port may vary depending on `launchSettings.json` settings)

## 📡 API Endpoints

### 1. Register New User
```
POST /api/Auth/register
Content-Type: application/json

{
    "username": "testuser",
    "password": "password123"
}
```

**Response:**
```json
{
    "id": "guid",
    "username": "testuser",
    "passwordHash": "...",
    "role": "",
    "refreshToken": "",
    "refreshTokenExpiryTime": "0001-01-01T00:00:00"
}
```

### 2. Login
```
POST /api/Auth/login
Content-Type: application/json

{
    "username": "testuser",
    "password": "password123"
}
```

**Response:**
```json
{
    "accessToken": "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9...",
    "refreshToken": "base64-encoded-refresh-token"
}
```

### 3. Refresh Access Token
```
POST /api/Auth/refresh-token
Content-Type: application/json

{
    "userId": "user-guid",
    "refreshToken": "refresh-token-from-login"
}
```

**Response:**
```json
{
    "accessToken": "new-jwt-token",
    "refreshToken": "new-refresh-token"
}
```

### 4. Protected Endpoint (Requires Authentication)
```
GET /api/Auth
Authorization: Bearer {accessToken}
```

### 5. Admin Only Endpoint
```
GET /api/Auth/Admin-Role
Authorization: Bearer {accessToken}
```

### 6. SuperAdmin Only Endpoint
```
GET /api/Auth/SuperAdmin-Role
Authorization: Bearer {accessToken}
```

### 7. User and Admin Endpoint
```
GET /api/Auth/UserAndAdmin-Role
Authorization: Bearer {accessToken}
```

## 📝 Usage Examples

### Using cURL

**Register a new user:**
```bash
curl -X POST https://localhost:5001/api/Auth/register \
  -H "Content-Type: application/json" \
  -d "{\"username\":\"testuser\",\"password\":\"password123\"}"
```

**Login:**
```bash
curl -X POST https://localhost:5001/api/Auth/login \
  -H "Content-Type: application/json" \
  -d "{\"username\":\"testuser\",\"password\":\"password123\"}"
```

**Use Access Token:**
```bash
curl -X GET https://localhost:5001/api/Auth \
  -H "Authorization: Bearer YOUR_ACCESS_TOKEN"
```

### Using Postman

1. Open Postman
2. Create a new Request
3. Select Method (POST/GET)
4. Enter URL: `https://localhost:5001/api/Auth/{endpoint}`
5. For Headers, add:
   - `Content-Type: application/json` (for POST requests)
   - `Authorization: Bearer {your-token}` (for Protected endpoints)

## 📁 Project Structure

```
AuthApp/
├── Controllers/
│   └── AuthController.cs          # Authentication controller
├── Services/
│   ├── IAuthService.cs            # Service interface
│   └── AuthService.cs             # Authentication service implementation
├── Entities/
│   └── User.cs                    # User entity model
├── Model/
│   ├── UserDto.cs                 # User DTO
│   ├── TokenResponseDto.cs        # Token response DTO
│   └── RefreshTokenRequestDto.cs  # Refresh token request DTO
├── Data/
│   └── AppDbContext.cs            # DbContext
├── Migrations/                    # Entity Framework Migrations
├── Program.cs                     # Application entry point
├── appsettings.json              # Application settings
└── AuthApp.csproj                # Project file
```

## ⚙️ Configuration

In `appsettings.json`:

```json
{
  "AppSettings": {
    "Token": "Your-Secret-Key-Here",      // Secret key for JWT signing
    "Issuer": "MyAwesomeApp",              // Token issuer
    "Audience": "MyAwesomeAudience"        // Token audience
  },
  "ConnectionStrings": {
    "UserDatabase": "Server=.;Database=UserDb;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

**⚠️ Important:** In production environment, you should:
- Use a strong and long secret key
- Store secret keys in `appsettings.Production.json` or use User Secrets
- Never commit sensitive configuration files to GitHub

## 🔒 Security

- Passwords are hashed using `PasswordHasher` from ASP.NET Identity
- JWT Tokens are signed and encrypted
- Refresh Tokens are separate from Access Tokens
- User permissions are protected with Role-Based Authorization

## 📚 Additional Notes

- **Access Token** is valid for one day (24 hours)
- **Refresh Token** is valid for 7 days
- New roles can be added to users through the database
- Swagger UI is only available in Development environment

## 🤝 Contributing

This is a simple educational project. You can:
- Add new features
- Improve security
- Add Unit Tests
- Improve documentation

## 📄 License

This project is open source and available for educational use.

---

**Made with ❤️ using ASP.NET Core**

---

*This README file was generated with the assistance of AI.*
