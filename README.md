---

# 📋 Task Management Application - Full Stack Project

A full-stack task management system built with **Angular**, **NgRx**, **ASP.NET Core Web API**, and **SQL Server**.  
The application allows users to create, edit, delete, and manage tasks and boards, leveraging modern front-end and back-end technologies.

[devchallenges](https://devchallenges.io/challenge/my-task-board-app)

---

## 🚀 Live Demo

🔗 [Coming Soon]

---

## 🛠️ Tech Stack

**Front-end:**
- [Angular](https://angular.io/)
- [NgRx](https://ngrx.io/) (State Management)
- [Tailwind CSS](https://tailwindcss.com/) (Styling)

**Back-end:**
- [ASP.NET Core Web API](https://dotnet.microsoft.com/en-us/apps/aspnet)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

**DevOps & Tools:**
- Git, GitHub
- Azure App Services / Azure SQL Database (for deployment)

---

## 🎨 Features

- Create, read, update, and delete **tasks** and **boards**.
- Edit task properties: **name, description, icon, status**.
- Default board setup with columns: **In Progress**, **Completed**, **Won't Do**.
- Auto-generate a **unique board ID**, accessible via `/board/:board-id`.
- Fully responsive and user-friendly interface.

---

## 📚 API Endpoints

| Method | Endpoint | Description |
|:------:|:--------:|:------------|
| GET | `/api/boards/:board-id` | Retrieve a specific board |
| POST | `/api/boards` | Create a new board |
| PUT | `/api/boards/:board-id` | Update a board's details |
| DELETE | `/api/boards/:board-id` | Delete a board |
| PUT | `/api/tasks/:task-id` | Update a task |
| DELETE | `/api/tasks/:task-id` | Delete a task |

---

## 🖥️ Local Setup Instructions

### Front-end (Angular)

```bash
git clone git@github.com:aekoky/devchallenges.io.git
cd devchallenges.io
docker-compose up --build
```

- Update `appsettings.json` to connect your SQL Server instance.
- Ensure CORS settings allow your frontend URL.

---

## 🎯 Learning Objectives

- Master full-stack development with Angular, NgRx, ASP.NET Core, and SQL Server.
- Implement RESTful API endpoints following clean architecture principles.
- Manage application state effectively using the Redux pattern.
- Deploy full-stack applications in a cloud environment (Azure recommended).
