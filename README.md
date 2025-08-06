# SalonReservaApi

API REST para la gestión de salones de cumpleaños.

## 🚀 Tecnologías

- .NET 8
- PostgreSQL (via Docker)
- EF Core
- Swagger

## 🧪 Endpoints

- `POST /api/reserva`: Crea una nueva reserva.
- `GET /api/reserva/{fecha}`: Lista las reservas para la fecha dada (YYYY-MM-DD).

## ▶️ Cómo correr

```bash
docker-compose up --build
```

La API estará disponible en http://localhost:5000

---

## 📝 Notas
- Valida solapamiento de reservas y 30 minutos entre turnos.
- Solo se puede reservar entre 9:00 y 18:00.