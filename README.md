[![Contributors][contributors-shield]][contributors-url][![Forks][forks-shield]][forks-url][![Stargazers][stars-shield]][stars-url][![Issues][issues-shield]][issues-url][![License][license-shield]][license-url][![LinkedIn][linkedin-shield]][linkedin-url]

# ![Logo][Logo] Dotnet.JwtAuth

A .NET 9 Web API template demonstrating secure JWT authentication and refresh token support for user registration and login.

## Description
Dotnet.JwtAuth is a modular, production-ready ASP.NET Core Web API project that implements JWT-based authentication and refresh tokens.
It provides endpoints for user registration, login, and token refresh, using Entity Framework Core for persistence.
The solution is organized into clean architecture layers for maintainability and extensibility.

## Features

## Getting Started

### Prerequisites
- Dotnet 9.0 or later

### Installation
The TirsvadWeb.JwtAuth can be installed in several ways:

#### Clone the repo
![Repo size][repos-size-shield]

1. **Clone the repository:**

    ```bash
    git clone https://github.com/TirsvadWeb/Dotnet.JwtAuth.git 
    cd Dotnet.JwtAuth
    ```

1. **Restore dependencies:**

    ```bash
    dotnet restore
    ```

1. **Update database (if needed):**

    ```bash
    dotnet ef database update --project src/JwtAuth.Infrastructure
    ```

1. **Build the project:**

    ```bash
    dotnet build
    ```

1. **Run the API:**

    ```bash
    dotnet run --project src/JwtAuth
    ```

## Configuration (`appsettings.json`)

Update your `src/JwtAuth/appsettings.json` with your JWT settings:

```json
{ 
    "ConnectionStrings": {
        "AuthDatabase": "Server=localhost\SQL2022EXPRESS;
        Database=JwtAuth;Trusted_Connection=true;
        TrustServerCertificate=true;"
    },
    "Jwt": {
        "Token": "YOUR_SECRET_KEY",
        "Issuer": "YOUR_ISSUER",
        "Audience": "YOUR_AUDIENCE"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*" }
```

- Replace `YOUR_SECRET_KEY`, `YOUR_ISSUER`, and `YOUR_AUDIENCE` with your own values.

## Example of Use

### Register a User

```http
POST /api/auth/register Content-Type: application/json
{ "userName": "testuser", "password": "TestPassword123!" }
```

### Login a User
```http
POST /api/auth/login Content-Type: application/json
{ "userName": "testuser", "password": "TestPassword123!" }
```

### Refresh Token
```http
POST /api/auth/refresh-token Content-Type: application/json
{ "userId": "USER_GUID", "refreshToken": "REFRESH_TOKEN" }
```

### Access Protected Endpoint
Add the JWT access token to the `Authorization` header:
```http
GET /api/auth Authorization: Bearer {accessToken}
```

## 📂 File Structure
```plaintext
Dotnet.JwtAuth/
├── 📄 docs/                         # Documentation files
│   └── 📄 doxygen/                  # Doxygen output
├── 🖼️ images/                       # Images used in documentation
├── 📂 src/                          # Source code for the library
│   └── 📦 JwtAuth/                  # ASP.NET Core Web API (entry point) 
│       ├── 📦 Controllers/          # API controllers
│       └── 📦 Data                  # Data access layer
│           └── 📦 Migrations        # Entity Framework migrations
└── 📂 tests                         # Unit tests for the library
    └── 📦 TestApi/                  # Unit tests for the API
```

## Contributing
Contributions are what make the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

See [CONTRIBUTING.md](CONTRIBUTING.md)

## Bug / Issue Reporting  
If you encounter a bug or have an issue to report, please follow these steps:  

1. **Go to the Issues Page**  
  Navigate to the [GitHub Issues page][githubIssue-url].  

2. **Click "New Issue"**  
  Click the green **"New Issue"** button to create a new issue.  

3. **Provide Details**  
  - **Title**: Write a concise and descriptive title for the issue.  
  - **Description**: Include the following details:  
    - Steps to reproduce the issue.  
    - Expected behavior.  
    - Actual behavior.  
    - Environment details (e.g., OS, .NET version, etc.).  
  - **Attachments**: Add screenshots, logs, or any other relevant files if applicable.  

4. **Submit the Issue**  
  Once all details are filled in, click **"Submit new issue"** to report it.  

## License
Distributed under the GPL-3.0 [License][license-url].

## Contact
Jens Tirsvad Nielsen - [LinkedIn][linkedin-url]

## Acknowledgments


<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/TirsvadWeb/Dotnet.JwtAuth?style=for-the-badge
[contributors-url]: https://github.com/TirsvadWeb/Dotnet.JwtAuth/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/TirsvadWeb/Dotnet.JwtAuth?style=for-the-badge
[forks-url]: https://github.com/TirsvadWeb/Dotnet.JwtAuth/network/members
[stars-shield]: https://img.shields.io/github/stars/TirsvadWeb/Dotnet.JwtAuth?style=for-the-badge
[stars-url]: https://github.com/TirsvadWeb/Dotnet.JwtAuth/stargazers
[issues-shield]: https://img.shields.io/github/issues/TirsvadWeb/Dotnet.JwtAuth?style=for-the-badge
[issues-url]: https://github.com/TirsvadWeb/Dotnet.JwtAuth/issues
[license-shield]: https://img.shields.io/github/license/TirsvadWeb/Dotnet.JwtAuth?style=for-the-badge
[license-url]: https://github.com/TirsvadWeb/Dotnet.JwtAuth/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://www.linkedin.com/in/jens-tirsvad-nielsen-13b795b9/
[githubIssue-url]: https://github.com/TirsvadWeb/Dotnet.JwtAuth/issues/
[repos-size-shield]: https://img.shields.io/github/repo-size/TirsvadWeb/Dotnet.JwtAuth?style=for-the-badg

[logo]: https://raw.githubusercontent.com/TirsvadWeb/Dotnet.JwtAuth/master/images/logo/32x32/logo.png
