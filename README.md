# StudentsManager

A web application built on .NET, React, MSSQL, Azure, and OpenAI, designed to manage course-related data at the University of Economics – Varna. Students actively use the platform while also developing new functionalities for it.

[![Docker Compose Build Check](https://github.com/profjordanov/students-manager/actions/workflows/docker-compose.yml/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/docker-compose.yml)
[![Deploy Manager to Azure App Service](https://github.com/profjordanov/students-manager/actions/workflows/dotnet-deploy.yml/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/dotnet-deploy.yml)
[![CodeQL](https://github.com/profjordanov/students-manager/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/profjordanov/students-manager/actions/workflows/github-code-scanning/codeql)
[![CodeFactor](https://www.codefactor.io/repository/github/profjordanov/students-manager/badge)](https://www.codefactor.io/repository/github/profjordanov/students-manager)
[![SonarQube Cloud](https://sonarcloud.io/images/project_badges/sonarcloud-light.svg)](https://sonarcloud.io/summary/new_code?id=profjordanov_students-manager)

---

## Table of Contents

- [Environments](#environments)
- [API Endpoints](#api-endpoints)
- [MVC Frontend](#mvc-frontend-mainjs--chatbot)
- [Technologies](#-technologies)
- [Project Structure](#-project-structure)
- [Getting Started](#️-getting-started)
- [Running Tests](#-running-tests)
- [Docker Commands](#-docker-commands)
- [License](#-license)
- [Security](#-security)

---

## Environments

| Environment | URL | Notes |
|---|---|---|
| Production | https://students-manager.site/ | Public production deployment |
| Development | https://students-manager-dev.azurewebsites.net/ | Dev backend / API deployment |
| React (SPA) | https://students-manager-spa.azurewebsites.net/ | React version of the platform |

## API Endpoints

### 1. Forum

**GET forum posts**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido?limit=20&skip=0'
```

**POST question**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido/question' \
  --header 'Content-Type: application/json' \
  --data '{
    "question": "api post get"
  }'
```

**POST comment**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido/comment' \
  --header 'Content-Type: application/json' \
  --data '{
    "forumQuestionId": 4,
    "description": "api post comment"
  }'
```

**GET questions**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/slido/questions?limit=20&skip=0'
```

Example response:

```json
["api post get", "lower api/slido/question", "because", "why?", "question", ".net 10"]
```

### 2. User Profile / Login

**POST login**

```bash
curl --request POST \
  --url https://students-manager-dev.azurewebsites.net/api/login \
  --header 'Content-Type: application/json' \
  --data '{
    "email": "jordan@abv.bg",
    "password": "password"
  }'
```

Responses:

```json
// 200
{
  "userId": "1eac9820-5e6e-4d10-6e94-08de36f40f78"
}
```

```json
// 401
{
  "message": "Invalid email or password."
}
```

**GET profile**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/students/profile/022a6007-f33c-47c3-b811-08de88b121f2'
```

Example response:

```json
// 200
{
  "id": "022a6007-f33c-47c3-b811-08de88b121f2",
  "fullName": "Dr J",
  "base64EncodePicture": null,
  "facultyNumber": "987987",
  "testQuestions": [
    {
      "testQuestionDescription": "Какъв е правилният синтаксис за препратка към външен скрипт, наречен xxx.js?",
      "questionOptionDescription": "<script src=xxx.js>",
      "wasCorrect": true
    }
  ]
}
```

```json
// 400
{
  "message": "Invalid user."
}
```

**PUT picture**

```bash
curl --location --request PUT 'https://students-manager-dev.azurewebsites.net/api/students/picture' \
  --header 'Content-Type: application/json' \
  --data '{
    "FacultyNumber": "123123123",
    "Password": "password",
    "Picture": "data:image/jpeg;base64,/9j/k="
  }'
```

### 3. Events

**POST event**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/events' \
  --header 'Content-Type: application/json' \
  --data '{
    "userId": "123123",
    "type": "test",
    "data": "temp"
  }'
```

**GET events by user ID**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/events/022a6007-f33c-47c3-b811-08de88b121f2'
```

Example response:

```json
[
  {
    "id": "fddea52a-5702-46c8-86f6-00ed51641660",
    "userId": "022a6007-f33c-47c3-b811-08de88b121f2",
    "datetimeUtc": "2026-03-24T14:33:42.3137213",
    "type": "geolocation-position-drj",
    "data": "{\"latitude\":53.513211150189285,\"longitude\":-80.50332253634296}"
  }
]
```

### 4. Chatbot

**POST save results**

```bash
curl "https://students-manager-dev.azurewebsites.net/api/chatbot/examination-answers" \
  -H "Content-Type: application/json" \
  --data-raw '{
    "userId": "022a6007-f33c-47c3-b811-08de88b121f2",
    "answers": [
      {
        "questionId": "q123123",
        "questionText": "What is programming?",
        "answer": "programming is .."
      }
    ]
  }'
```

**GET examination answers**

```bash
curl --location 'https://students-manager-dev.azurewebsites.net/api/chatbot/examination-answers/522a6007-fkkc-47c3-b811-08de88b121f2'
```

---

## MVC Frontend (main.js + chatbot)

### main.js

`main.js` is a single bundled and minified file that mixes third-party libraries with a custom global `App` namespace (site logic).

#### Third-party libraries embedded in the bundle

| Library | Version | Purpose |
|---------|---------|---------|
| jQuery | 3.1.1 | DOM manipulation |
| GSAP TweenMax | 1.19.x | Animations (ScrollToPlugin, CSSPlugin, etc.) |
| ScrollMagic | 2.0.5 | Scroll-triggered animations (plus GSAP plugin) |
| Blazy | — | Lazy-loading images |
| fullPage.js | — | One-page scrolling sections |
| Swiper | — | Carousel / slider |
| Plyr | — | Video / audio player |
| jQBrowser | — | User-agent detection helper |

#### Custom application logic

The bundle defines a global `App` object and initializes multiple modules via `App.init()`.

On window load it calls:

| Module | Description |
|--------|-------------|
| `App.resize()` | Sets `App.viewport_height` / `App.viewport_width` and `App.mobile` based on UA / width |
| `App.bind()` | Attaches UI handlers (menu, category tabs, video popup, job popup, chatbot start, etc.) |
| `App.UI.init()` | Lazy-loading + fullPage initialization |
| `App.Test.init()` | Course tests behavior |
| `App.Scroll.init()` | Parallax + header scroll states + scroll-to |
| `App.sliderSwipper.init()` | Initializes Swiper sliders |
| `App.Animations`, `App.Login`, `App.Profile` | Additional UI modules |

### Course Tests Page

The key module is `App.Test`. It binds click handlers on the active question only:

- `#test` click on `.question.active .answer input` → `animateAfterClick`
- `#test` click on `.question.active .answer input` → `countStats`

**`countStats()` behavior:**

- Reads the value of the clicked radio (`action` / `process` / `people` / `idea`)
- Reads `data-answer` (`1` or `2`) and `data-question` (`1..40`)
- Resets / recomputes category totals
- Records the chosen answer for that question

**`animateAfterClick()` behavior:**

- Hides answers for non-active questions initially (`setOpacityToAllUnactiveQuestions`)
- After selecting an answer, animates the transition to the next `.question` (via TweenMax)
- Uses a guard (`#test.animating`) to prevent double-clicks during transitions

### Chatbot

#### External dependencies

| Library | Bundled? | Purpose |
|---------|----------|---------|
| Lodash (`_`) | Yes | Utility functions (`_.trim`, `_.map`, `_.filter`, etc.) |
| Typed.js | Yes | Typing animation effect (`new Typed(...)`) |
| jQuery (`$`) | No | Expected to be available globally |

---

## 🚀 Technologies

- **Backend:** ASP.NET Core MVC
- **Frontend:** React (SPA), Razor Views
- **Database:** Microsoft SQL Server
- **Cloud:** Azure App Service
- **AI:** OpenAI
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

---

## 📄 License

See [LICENSE](LICENSE) for details.

## 🔒 Security

See [SECURITY.md](SECURITY.md) for security policies.
