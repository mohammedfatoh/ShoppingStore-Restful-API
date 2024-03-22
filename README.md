# ShoppingStore API #
ShoppingStore API is Web Service Provider services for Consumer To Consume it, API adopts an N-tier architecture,
Useing ASP.NET Core 8, Entity Framework Core,and MVC patterns.
The RESTful API backend ensures efficient communication, 
while JWT authentication and role-based authorization enhance security.

# Technologies
- ASP.NET Core 8
- Entity Framework Core
- MVC Pattern
- RESTful APIs
- MS SQL Server
- JWT Authentication
- Identity for User Management
- DataMapper
# Architecture
# ShoppingStore follows a robust N-tier architecture, which includes:
 - Business Layer(API): Implements core business logic.
 - Data Access Layer: Utilizes the Repository and Unit of Work patterns for efficient data retrieval.
 - Repository Pattern: Organizes data access logic.
 - Unit of Work Pattern: Manages transactions.
 - Dependency Injection: Enhances code modularity and testability.
# Backend
 - The RESTful APIs provide JSON responses
 - CRUD APIs for Main Business Entities: Facilitates Create, Read, Update, and Delete operations.
    Search, Sort, and Filter APIs: Enhances data retrieval capabilities.
