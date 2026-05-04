# AulaControl - Sistema de préstamo de equipos universitarios

# AulaControl

AulaControl es un sistema web para gestionar el préstamo de equipos universitarios.  
El proyecto permite registrar estudiantes, registrar equipos, crear préstamos, devolver equipos y consultar el estado general del sistema.

Este sistema fue desarrollado como proyecto académico para aplicar conceptos de backend, frontend, base de datos, control de versiones y dockerización.

---

## Tecnologías utilizadas

### Backend
- .NET Web API
- C#
- Entity Framework Core
- Pomelo Entity Framework Core MySQL
- Swagger

### Frontend
- Angular
- TypeScript
- HTML
- CSS

### Base de datos
- MySQL
- MySQL Workbench

### Contenedores
- Docker
- Docker Compose

### Control de versiones
- Git
- GitHub
- Ramas: `main`, `develop`, `feature/backend`, `feature/frontend`, `feature/dockerizacion`

---

## ¿Qué hace AulaControl?

El sistema permite administrar el préstamo de equipos dentro de una institución educativa.

Las funciones principales son:

1. Registrar estudiantes.
2. Editar estudiantes.
3. Eliminar estudiantes, siempre que no tengan préstamos activos.
4. Registrar equipos.
5. Editar equipos.
6. Eliminar equipos, siempre que no tengan préstamos activos.
7. Registrar préstamos.
8. Devolver préstamos.
9. Cambiar automáticamente el estado del equipo.
10. Consultar el resumen general del sistema.

---
## Funcionamiento general del sistema

El sistema está dividido en tres partes principales:

```text
Frontend Angular  →  Backend .NET API  →  Base de datos MySQL

Estructura del proyecto

AulaControl/
├── AulaControl.Api/
│   ├── Controllers/
│   ├── Data/
│   ├── DTOs/
│   ├── Migrations/
│   ├── Models/
│   ├── Seed/
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
│
├── aula-control-front/
│   ├── src/
│   │   ├── app/
│   │   │   ├── models/
│   │   │   ├── pages/
│   │   │   ├── services/
│   │   │   ├── app.config.ts
│   │   │   ├── app.routes.ts
│   │   │   └── app.html
│   │   └── main.ts
│   ├── Dockerfile
│   └── nginx.conf
│
├── docker-compose.yml
└── README.md

