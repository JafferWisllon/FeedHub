# 🚀 FeedHub

FeedHub is an RSS Feed API built with ASP.NET Core.
The application allows users to register RSS feeds, synchronize external RSS content, persist feed items, and retrieve paginated feed data through a REST API.

---

## ✨ Features

### 📡 Feed Management
- Create feeds
- List all feeds
- Get feed by id

### 🔄 RSS Synchronization
- Refresh RSS feeds from external URLs
- Parse RSS items using `CodeHollow.FeedReader`
- Persist RSS items into database

### 📰 Feed Items
- Retrieve feed items by feed id
- Paginated feed items endpoint
- Sort feed items by publish date
- Prevent duplicated feed items

### ⚠️ Error Handling
- Global exception middleware
- Custom business exceptions
- Standardized API error responses

### 🧱 Data Integrity
- Application-level deduplication
- Database unique composite index (`FeedId + Link`)

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
- Paginated responses
- Multi-layer deduplication strategy

---

## ▶️ Running Locally

### ✅ Requirements

- .NET SDK
- SQL Server

---

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

#### Get paginated feed items

```http
GET /feeds/{id}/items?page=1&pageSize=20
```

#### Refresh RSS feed

```http
POST /feeds/{id}/refresh
```

---

## 📄 Example Paginated Response

```json
{
  "page": 1,
  "pageSize": 20,
  "totalCount": 135,
  "totalPages": 7,
  "nextPage": "GET /feeds/1/items?page=2&pageSize=20",
  "items": [
    {
      "title": "Example RSS Item",
      "link": "https://example.com/article",
      "publishAt": "2026-05-15T10:00:00Z"
    }
  ]
}
```

---

## 🧭 Roadmap

### 🚀 Next Features
- Authentication & Authorization
- Unit tests
- Logging
- Docker support
- Docker Compose
- CI/CD pipeline

### 🌟 Future Improvements
- API versioning
- Background jobs for automatic refresh
- Health checks
- Filtering & search
- Rate limiting
- Caching

---

## 📌 Status

🚧 Active development
```