# StudentsManager

A web application built on .NET and Azure, designed to manage course-related data at the University of Economics – Varna. Students actively use the platform while also developing new functionalities for it.

## 🚀 Technologies

- **Framework:** ASP.NET Core MVC
- **Containerization:** Docker & Docker Compose
- **Testing:** xUnit (StudentsManager.Tests)

## 📁 Project Structure

```
├── StudentsManager.Mvc/          # Main MVC application
│   ├── Controllers/              # MVC Controllers
│   ├── Domain/                   # Domain models
│   ├── Mappings/                 # Object mappings
│   ├── Migrations/               # Database migrations
│   ├── Persistence/              # Data access layer
│   ├── Services/                 # Business logic services
│   ├── Settings/                 # Configuration settings
│   ├── Views/                    # Razor views
│   └── wwwroot/                  # Static files
├── StudentsManager.Tests/        # Unit tests
└── docker-compose.yml            # Docker orchestration
```

## 🛠️ Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) (optional)

### Running Locally

```bash
cd StudentsManager.Mvc
dotnet run
```

### Running with Docker

```bash
./run-app.sh
```

Or using Docker Compose directly:

```bash
docker-compose up
```

### Stopping the Application

```bash
./run-down-app.sh
```

## 🧪 Running Tests

```bash
./run-tests.sh
```

Or manually:

```bash
dotnet test StudentsManager.Tests/
```

## 🐳 Docker Commands

| Script | Description |
|--------|-------------|
| `run-app.sh` | Start the application |
| `run-down-app.sh` | Stop the application |
| `push-app.sh` | Push Docker images |
| `run-tests.sh` | Run test suite |

## 📄 License

See [LICENSE](LICENSE) for details.

## 🔒 Security

See [SECURITY.md](SECURITY.md) for security policies.
