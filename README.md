# 🚀 FeedHub

FeedHub is an RSS Feed API built with ASP.NET Core.
The application allows users to register RSS feeds, refresh external feed content, and retrieve feed items through a REST API.

---

## ✨ Features

### 📡 Feed Management
- Create feeds
- List all feeds
- Get feed by id

### 🔄 RSS Integration
- Refresh RSS feeds from external URLs
- Parse RSS items using `CodeHollow.FeedReader`

### 📰 Feed Items
- Retrieve feed items by feed id

### ⚠️ Error Handling
- Global exception middleware
- Custom business exceptions
- Standardized API error responses

---

## 🛠️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- CodeHollow.FeedReader

---

## 📁 Project Structure

```plaintext
FeedHub.API
├── Controllers
├── Data
│   ├── Configurations
│   ├── Context
│   └── Migrations
├── Dtos
├── Exceptions
├── Mappings
├── Middlewares
├── Models
├── Services
│   ├── Interfaces
│   ├── FeedService
│   └── RssService
├── Validators
└── Program.cs
```

---

## 🧱 Architecture Notes

The project follows a simple and pragmatic architecture with focus on:
- Separation of responsibilities
- Clear service boundaries
- Incremental refactoring
- Maintainability
- Readability

Current architectural decisions include:
- Dedicated RSS integration service
- Global exception handling middleware
- DTO mapping separation
- Validation extracted from services

---

## ▶️ Running Locally

### ✅ Requirements

- .NET SDK
- SQL Server


### 📦 Restore packages

```bash
dotnet restore
```

### 🗄️ Apply migrations

```bash
dotnet ef database update
```

### ▶️ Run the application

```bash
dotnet run
```

---

## 🌐 Main Endpoints

### 📡 Feeds

#### Create feed

```http
POST /feeds
```

#### Get all feeds

```http
GET /feeds
```

#### Get feed by id

```http
GET /feeds/{id}
```

---

### 📰 Feed Items

#### Get feed items

```http
GET /feeds/{id}/items
```

#### Refresh RSS feed

```http
POST /feeds/{id}/refresh
```

---

## 🧭 Roadmap

- Persist RSS items into database
- Deduplication strategy
- Pagination
- Unit tests
- Docker support
- CI/CD pipeline
- Logging
- Authentication
- API versioning

---

## 📌 Status

🚧 Active development