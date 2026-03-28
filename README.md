# Employee Management API

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

## Overview

ASP.NET Core Web API using SQL Server and Entity Framework Core with layered architecture and CRUD endpoints.

## Tech Stack

- **Framework**: ASP.NET Core 10.0
- **Language**: C#
- **Database**: SQL Server 2025
- **ORM**: Entity Framework Core
- **API Documentation**: Swagger/OpenAPI
- **Architecture**: Layered (Controller → Service → Repository)

## Features

✅ CRUD operations for employees  
✅ Layered backend architecture  
✅ Entity Framework Core code-first migrations  
✅ Swagger UI for API testing  
✅ Duplicate email prevention  
✅ PATCH API for partial updates  
✅ Comprehensive error handling  
✅ Async/await patterns  

## Architecture
Client / Swagger UI
↓
Controller (HTTP)
↓
Service (Business Logic)
↓
Repository (Data Access)
↓
Entity Framework Core / DbContext
↓
SQL Server

text

### Layer Responsibilities

| Layer | Responsibility |
|-------|-----------------|
| **Controller** | Handles HTTP requests/responses, routing |
| **Service** | Business logic, validations, duplicate checks |
| **Repository** | Database operations, EF Core abstraction |
| **DbContext** | SQL Server communication |

## Project Structure
EmployeeManagementAPI/
├── Controllers/
│ └── EmployeeController.cs # HTTP endpoints
├── Services/
│ ├── IEmployeeService.cs # Service interface
│ └── EmployeeService.cs # Business logic
├── Repositories/
│ ├── IEmployeeRepository.cs # Repository interface
│ └── EmployeeRepository.cs # Data access
├── Models/
│ ├── Employee.cs # Entity model
│ ├── CreateEmployeeDto.cs # Create DTO
│ └── UpdateEmployeePatchDto.cs # Update DTO
├── Data/
│ └── ApplicationDbContext.cs # EF Core DbContext
├── Migrations/
│ └── [Migration files]
├── appsettings.json # Configuration
├── Program.cs # Dependency injection
└── EmployeeManagementAPI.csproj

text

## Database Schema

```sql
CREATE TABLE [dbo].[Employees] (
    [EmployeeId] INT PRIMARY KEY IDENTITY(1,1),
    [Name] VARCHAR(100) NOT NULL,
    [Department] VARCHAR(50) NOT NULL,
    [Salary] DECIMAL(18,2) NOT NULL,
    [Email] VARCHAR(100) NOT NULL UNIQUE,
    [CreatedDate] DATETIME2 NOT NULL
);
```

## API Endpoints

### Base URL
https://localhost:7058/api/employee

text

### GET All Employees
```http
GET /api/employee
```

### GET Employee by ID
```http
GET /api/employee/{id}
```

### CREATE Employee
```http
POST /api/employee
Content-Type: application/json

{
  "name": "Vineeth",
  "department": "IT",
  "salary": 50000,
  "email": "vineeth@example.com"
}
```

### UPDATE Full (PUT)
```http
PUT /api/employee/{id}
Content-Type: application/json

{
  "name": "Vineeth Updated",
  "department": "HR",
  "salary": 60000,
  "email": "vineeth.updated@example.com"
}
```

### UPDATE Partial (PATCH)
```http
PATCH /api/employee/{id}
Content-Type: application/json

{
  "salary": 65000
}
```

### DELETE Employee
```http
DELETE /api/employee/{id}
```

## Setup & Installation

### Prerequisites
- .NET 10.0 SDK
- SQL Server 2019+
- Visual Studio 2022 or VS Code
- Git

### Steps

1. **Clone repository**
```bash
git clone https://github.com/srinagavineeth/EmployeeManagementAPI.git
cd EmployeeManagementAPI
```

2. **Restore dependencies**
```bash
dotnet restore
```

3. **Update connection string** (appsettings.json)
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;Encrypt=False;"
}
```

4. **Apply migrations**
```bash
dotnet ef database update
```

5. **Run application**
```bash
dotnet run
```

6. **Open Swagger UI**
https://localhost:7058/swagger

text

## Testing with cURL

```bash
# Get all employees
curl -X GET https://localhost:7058/api/employee

# Get employee by ID
curl -X GET https://localhost:7058/api/employee/1

# Create employee
curl -X POST https://localhost:7058/api/employee \
  -H "Content-Type: application/json" \
  -d '{"name":"Test User","department":"IT","salary":50000,"email":"test@example.com"}'

# Update employee (PUT)
curl -X PUT https://localhost:7058/api/employee/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"Updated","department":"HR","salary":55000,"email":"updated@example.com"}'

# Partial update (PATCH)
curl -X PATCH https://localhost:7058/api/employee/1 \
  -H "Content-Type: application/json" \
  -d '{"salary":65000}'

# Delete employee
curl -X DELETE https://localhost:7058/api/employee/1
```

## Code Quality

✅ Async/await patterns  
✅ Exception handling  
✅ Input validation  
✅ Logging  
✅ Repository pattern  
✅ Service pattern  
✅ DTO pattern  
✅ Dependency injection  

## Error Handling

| Status Code | Meaning |
|-------------|---------|
| 200 | OK - Success |
| 201 | Created - POST success |
| 204 | No Content - PUT/PATCH/DELETE success |
| 400 | Bad Request - Invalid input |
| 404 | Not Found - Resource doesn't exist |
| 409 | Conflict - Duplicate email |
| 500 | Server Error |

## Future Enhancements

- [ ] JWT Authentication & Authorization
- [ ] Role-based access control
- [ ] Angular frontend UI
- [ ] Unit tests (xUnit)
- [ ] Integration tests
- [ ] Pagination & filtering
- [ ] Docker containerization
- [ ] GitHub Actions CI/CD
- [ ] Azure deployment

## Security Notes

⚠️ Never store real passwords or secrets in `appsettings.json` before pushing to GitHub  
✅ Use `appsettings.Development.json` for local secrets  
✅ Use environment variables for production secrets  

## Contributing

1. Fork the repository
2. Create feature branch (`git checkout -b feature/NewFeature`)
3. Commit changes (`git commit -m 'Add NewFeature'`)
4. Push to branch (`git push origin feature/NewFeature`)
5. Open Pull Request

## License

MIT License

## Author

**Vineeth**
- GitHub: [@srinagavineeth](https://github.com/srinagavineeth)
- Full-stack .NET developer with 4.7+ years experience
- Specialization: ASP.NET Core, Angular, SQL Server
- Open to freelancing & opportunities

## Acknowledgments

- Microsoft .NET Team
- Entity Framework Core documentation
- ASP.NET Core community

---

**Status**: ✅ Production Ready  
**Version**: 1.0.0  
**Architecture**: Layered (Controller → Service → Repository)  
**Last Updated**: March 28, 2026
