# Employee Management API

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

## Overview

A **production-ready** ASP.NET Core Web API demonstrating full-stack development with SQL Server and Entity Framework Core. Complete CRUD operations, Swagger documentation, error handling, and logging.

## Features

- ✅ RESTful API Endpoints (GET, POST, PUT, DELETE)
- ✅ SQL Server Integration with proper schema
- ✅ Entity Framework Core with Code-First migrations
- ✅ Swagger/OpenAPI interactive documentation
- ✅ Comprehensive error handling & logging
- ✅ DTO pattern for separation of concerns
- ✅ Input validation & business logic
- ✅ Clean Git history & structure

## Tech Stack

| Component | Technology | Version |
|-----------|-----------|---------|
| Framework | ASP.NET Core | 10.0 |
| Language | C# | Latest |
| Database | SQL Server | 2025 |
| ORM | Entity Framework Core | Latest |
| API Docs | Swagger/OpenAPI | v3 |

## Project Architecture
EmployeeManagementAPI/
├── Controllers/
│ └── EmployeeController.cs
├── Models/
│ ├── Employee.cs
│ ├── CreateEmployeeDto.cs
│ └── ApplicationDbContext.cs
├── appsettings.json
├── Program.cs
└── EmployeeManagementAPI.csproj

text

## Prerequisites

- .NET 10.0 SDK
- SQL Server 2019+
- Visual Studio 2022 or VS Code
- Git

## Installation

1. Clone repository
```bash
git clone https://github.com/srinagavineeth/EmployeeManagementAPI.git
cd EmployeeManagementAPI
```

2. Restore dependencies
```bash
dotnet restore
```

3. Update appsettings.json with connection string
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;Encrypt=False;"
}
```

4. Apply migrations
```bash
dotnet ef database update
```

5. Run application
```bash
dotnet run
```

6. Access Swagger UI
https://localhost:7058/swagger

text

## API Endpoints

### Base URL
https://localhost:7058/api/employee

text

### Get All Employees
```http
GET /api/employee
```

### Get Employee by ID
```http
GET /api/employee/{id}
```

### Create Employee
```http
POST /api/employee
Content-Type: application/json

{
  "name": "John Doe",
  "department": "IT",
  "salary": 50000,
  "email": "john@company.com"
}
```

### Update Employee
```http
PUT /api/employee/{id}
Content-Type: application/json

{
  "name": "John Updated",
  "department": "HR",
  "salary": 55000,
  "email": "john.updated@company.com"
}
```

### Delete Employee
```http
DELETE /api/employee/{id}
```

## Testing with cURL

```bash
# Get all employees
curl -X GET https://localhost:7058/api/employee

# Create employee
curl -X POST https://localhost:7058/api/employee \
  -H "Content-Type: application/json" \
  -d '{"name":"Test User","department":"IT","salary":50000,"email":"test@company.com"}'

# Get specific employee
curl -X GET https://localhost:7058/api/employee/1

# Update employee
curl -X PUT https://localhost:7058/api/employee/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"Updated","department":"HR","salary":55000,"email":"updated@company.com"}'

# Delete employee
curl -X DELETE https://localhost:7058/api/employee/1
```

## Database Schema

```sql
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] INT PRIMARY KEY IDENTITY(1,1),
    [Name] VARCHAR(100) NOT NULL,
    [Department] VARCHAR(50) NOT NULL,
    [Salary] DECIMAL(18,2) NOT NULL,
    [Email] VARCHAR(100),
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE()
);
```

## Error Handling

| Status Code | Meaning |
|-------------|---------|
| 200 | OK - Successful GET |
| 201 | Created - Successful POST |
| 204 | No Content - Successful PUT/DELETE |
| 400 | Bad Request - Invalid input |
| 404 | Not Found - Resource not found |
| 500 | Server Error |

## Code Quality

✅ Async/await for I/O operations
✅ Proper exception handling
✅ Input validation
✅ Logging implementation
✅ HTTPS enforcement
✅ Connection pooling

## Future Enhancements

- [ ] JWT Authentication
- [ ] Role-based access control
- [ ] Angular frontend
- [ ] Unit tests (xUnit)
- [ ] Docker containerization
- [ ] GitHub Actions CI/CD
- [ ] Azure deployment

## Contributing

1. Fork the repository
2. Create feature branch
3. Commit changes
4. Push to branch
5. Open Pull Request

## License

MIT License

## Author

**Srina Nani**
- GitHub: [@srinagavineeth](https://github.com/srinagavineeth)
- Full-stack .NET developer with 4.7+ years experience

## Acknowledgments

- Microsoft .NET Team
- Entity Framework Core documentation
- ASP.NET Core community

---

**Status:** ✅ Production Ready
**Version:** 1.0.0
**Last Updated:** March 28, 2026
