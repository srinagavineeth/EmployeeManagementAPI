# Employee Management API

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge\&logo=c-sharp\&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge\&logo=dotnet\&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge\&logo=microsoft-sql-server\&logoColor=white)

---

## 🚀 Overview

Production-ready ASP.NET Core Web API with JWT authentication, pagination, search, filtering, and performance optimizations using SQL Server and Entity Framework Core.

---

## 🛠 Tech Stack

* **Framework**: ASP.NET Core 10.0
* **Language**: C#
* **Database**: SQL Server
* **ORM**: Entity Framework Core
* **Authentication**: JWT (JSON Web Token)
* **API Docs**: Swagger / OpenAPI
* **Architecture**: Layered (Controller → Service → Repository)

---

## 🔥 Features

### ✅ Core

* CRUD operations for employees
* Layered architecture
* DTO-based request models
* Duplicate email validation

### 🔐 Authentication

* JWT-based authentication
* BCrypt password hashing
* Secure token generation

### ⚡ Performance Optimizations

* Pagination with limits
* Search with indexed queries (`LIKE`)
* Filtering with pagination
* `AsNoTracking()` for faster reads
* Optimized queries (no full table scan)

### 🧠 Advanced API Design

* Standardized API response (`ApiResponse<T>`)
* Global exception handling (Middleware)
* Consistent HTTP status codes
* Clean separation of concerns

---

## 🏗 Architecture

Client / Swagger
↓
Controller (HTTP layer)
↓
Service (Business logic)
↓
Repository (Data access)
↓
Entity Framework Core
↓
SQL Server

---

## 📂 Project Structure

EmployeeManagementAPI/
├── Controllers/
├── Services/
├── Repositories/
├── Models/
├── Middleware/
├── Data/
├── Migrations/
├── Program.cs
└── appsettings.json

---

## 🗄 Database Schema

```sql
CREATE TABLE Employees (
    EmployeeId INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100),
    Department VARCHAR(50),
    Salary DECIMAL(18,2),
    Email VARCHAR(100) UNIQUE,
    CreatedDate DATETIME2
);
```

---

## 🔗 API Endpoints

### 🔐 Authentication

```http
POST /api/authentication/login
```

---

### 👨‍💼 Employees

```http
GET    /api/employee?pageNumber=1&pageSize=10
GET    /api/employee/{id}
POST   /api/employee
PUT    /api/employee/{id}
PATCH  /api/employee/{id}
DELETE /api/employee/{id}
```

---

### 🔍 Search

```http
GET /api/employee/search?keyword=Employee_1
```

---

### 🎯 Filter + Pagination

```http
GET /api/employee/filter?department=IT&pageNumber=1&pageSize=10
```

---

## 📊 Sample Response

```json
{
  "success": true,
  "message": "Employees fetched successfully",
  "data": [...],
  "pageNumber": 1,
  "pageSize": 10,
  "totalCount": 100000
}
```

---

## ⚡ Performance Highlights

* Handles 100,000+ records efficiently
* Optimized search using indexed queries
* Controlled response size (max 50 records)
* Prevents API abuse with pagination limits

---

## ⚙️ Setup & Run

```bash
git clone https://github.com/srinagavineeth/EmployeeManagementAPI.git
cd EmployeeManagementAPI
dotnet restore
dotnet ef database update
dotnet run
```

Swagger:

```
https://localhost:7058/swagger
```

---

## 🧪 Testing

Supports:

* Swagger UI
* Postman
* cURL

---

## 🧠 Code Quality

* Clean architecture
* Async/await
* Dependency injection
* Validation
* Logging
* Exception middleware
* Scalable design

---

## 🔐 Security

* JWT Authentication
* Password hashing with BCrypt
* No plain-text passwords

---

## 🚀 Future Enhancements

* Role-based authorization
* Angular frontend
* Unit & integration tests
* Docker support
* CI/CD pipeline
* Cloud deployment

---

## 👨‍💻 Author

**Vineeth**

* 4.7+ years experience
* .NET Full Stack Developer
* Open to freelancing

---

## 📌 Status

⚡ Optimized backend API
📦 Version: 1.1.0
📅 Last Updated: March 29, 2026
