<img src="https://github.com/user-attachments/assets/07b3ff93-2118-4bfc-b0a0-ca24334b9080" width=120> <br>
[@maxshtefec](https://www.linkedin.com/in/maxshtefec/) :+1: Es el creador y donador de este fabuloso repositorio!

# API de Gestión de Salones de Cumpleaños 🎉

Este proyecto es una API REST desarrollada en **.NET 8** que permite la gestión de reservas de salones para cumpleaños. Cumple con una serie de restricciones horarias, evita solapamientos entre eventos y permite consultar la disponibilidad por fecha.

## ✨ Características principales

- Crear reservas de salones con validaciones de negocio
- Consultar reservas por fecha o todas las reservas
- Validaciones:
  - Hora de inicio < hora de fin
  - Horario permitido entre las 9:00 y 18:00 hs
  - Margen obligatorio de 30 minutos entre reservas
- Base de datos PostgreSQL ejecutada en Docker
- Documentación con Swagger integrada
- Estructura clara y mantenible con DTOs, EF Core y validaciones
- Código limpio, desacoplado y con buenas prácticas

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- ASP.NET Core
- Entity Framework Core
- PostgreSQL (con Docker)
- Swagger
- C# 12

---

## 🧱 Arquitectura

El proyecto sigue una arquitectura **por capas**, separando responsabilidades en:

- `Models/` → Entidades de dominio (`Reserva`, `Salon`)
- `Dtos/` → Objetos de transferencia de datos (`ReservaDto`, `SalonDto`)
- `Data/` → Contexto de base de datos (`AppDbContext`)
- `Controllers/` → Controladores REST
- `Program.cs` → Configuración de servicios, middlewares y migraciones

Se aplicaron principios SOLID y buenas prácticas en el manejo de errores, validaciones y responses HTTP.

---

## 📄 Endpoints

| Método | Endpoint              | Descripción |
|--------|-----------------------|-------------|
| POST   | `/api/reserva`        | Crear una reserva con validaciones |
| GET    | `/api/reserva`        | Obtener todas las reservas |
| GET    | `/api/reserva/{fecha}`| Obtener reservas de una fecha específica (formato `yyyy-MM-dd`) |

---

## 📦 Instalación y ejecución

### 1. Clonar el repositorio

```bash
git clone https://github.com/tu-usuario/salon-reservas-api.git
cd salon-reservas-api
```

### 2. Crear la imagen de Docker y levantar servicios

```bash
docker compose up --build
```

Esto ejecutará el contenedor con PostgreSQL y levantará la API en el puerto `http://localhost:5000`

### 3. Acceder a la documentación Swagger

Ir a [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🧪 Pruebas unitarias

Actualmente, se espera cubrir el controlador `ReservaController` con pruebas unitarias para validar:

- Solapamiento de reservas
- Validaciones de horario permitido
- Validación de márgenes entre reservas

(Implementación opcional aún no incluida en este repositorio)

---

## 🚀 Consideraciones adicionales

- La base de datos incluye 3 salones precargados (`Salón A`, `Salón B`, `Salón C`).
- Las migraciones se aplican automáticamente al iniciar la aplicación.
- En lugar de una base en memoria, se optó por una base real (PostgreSQL) para demostrar el manejo con Docker.

---

## 💬 Autor

Desarrollado como solución a un desafío técnico. Se aplicaron buenas prácticas de desarrollo backend en .NET, arquitectura por capas, validaciones de negocio y documentación integrada.

---

## 🐳 Docker Compose

Ejemplo de archivo `docker-compose.yml`:

```yaml
version: '3.4'

services:
  salon-db:
    image: postgres:latest
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: salon_reservas
    ports:
      - "5432:5432"
    volumes:
      - pgdata:/var/lib/postgresql/data

  salon-api:
    build: .
    depends_on:
      - salon-db
    ports:
      - "5000:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development

volumes:
  pgdata:
```

---

## ✅ Estado del proyecto

✅ Funcionalidad completa implementada  
✅ Validaciones de negocio agregadas  
✅ Docker y Swagger configurados  
🔜 Pruebas unitarias (por agregar)  
🔜 Interfaz frontend (opcional no incluida)

---

## 📝 Licencia

Este proyecto es de uso libre con fines educativos y demostrativos.
