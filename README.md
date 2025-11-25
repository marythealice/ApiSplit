Overview

API built with ASP.NET Core (.NET 9) for managing perfume decants. Uses PostgreSQL (running in Docker).

Quick start
1) Start PostgreSQL with Docker
```
docker run --name decant-db \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=mysecretpassword \
  -e POSTGRES_DB=postgres \
  -p 5432:5432 \
  -d postgres:latest
```
(or ```docker compose up -d``` if you have a compose file)

2) Connection string

Put this in appsettings.json (or env var) — your project key is perfume_db:
```
"ConnectionStrings": {
  "perfume_db": "User ID=postgres;Password=mysecretpassword;Host=localhost;Port=5432;Database=postgres;"
}
```
3) Recreate migrations (you deleted them)

If you removed migrations and want a fresh start:

```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
4) Run the API
```dotnet run```
