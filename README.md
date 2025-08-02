# Exchange Rate Comparer

Este proyecto forma parte de una prueba técnica. Su objetivo es consultar 3 APIs diferentes de tipo de cambio y devolver la mejor oferta (monto convertido más alto).

## 🧩 Arquitectura

* .NET 9.0 (preview)
* Patrón SOLID (estrategia por API)
* Microservicios (cada API es un contenedor Docker)
* Proyecto principal `ExchangeRateComparer` consume las 3
* Swagger habilitado (`/swagger`)
* Pruebas unitarias con xUnit y Moq

## 🚀 Cómo levantar el proyecto

### Requisitos:

* Docker y Docker Compose
* SDK .NET 9.0 Preview instalado localmente (si compilas fuera de Docker)

### 1. Clona el proyecto

```bash
git clone https://github.com/teresavargas19/ExchangeRateApis.git
cd ExchangeRateApis
```

### 2. Levanta todo con Docker Compose

```bash
docker-compose build
docker-compose up
```

Esto construirá y levantará:

* `api1` en `http://localhost:5001/random1`
* `api2` en `http://localhost:5002/random2`
* `api3` en `http://localhost:5003/random3`
* `comparer` en `http://localhost:5000/compare`

### 3. Accede a Swagger

```http
http://localhost:5000/swagger
```

Aquí podrás probar `GET /compare` visualmente.

## 📌 Ejemplo de respuesta

```json
{
  "sourceApi": "Api3",
  "convertedAmount": 91.23
}
```

## 🧪 Pruebas

### Ejecutar pruebas localmente:

```bash
cd ExchangeRateComparerApp.Tests
 dotnet test
```

Incluye pruebas que validan que `ExchangeRateService` elige correctamente la mejor tasa entre varias estrategias.

## 📦 Estructura del proyecto

```
├── Api1/Api2/Api3           → Microservicios simulados
├── ExchangeRateComparer  → Lógica principal
│   ├── Controllers           → CompareController.cs
│   ├── Services              → ExchangeRateService.cs
│   ├── Strategies            → Api1/2/3Strategy.cs
├── ExchangeRateComparer.Tests → xUnit + Moq
├── docker-compose.yml       → Orquestación completa
```

## ✅ Completado

* [x] Swagger UI
* [x] Docker Compose
* [x] Principios SOLID
* [x] Logs
* [x] Unit Tests
* [x] Compatible con .NET 9

---

## 📬 Contacto

Para cualquier pregunta técnica, puedes escribirme a: `vargasbrito19@gmail.com`
