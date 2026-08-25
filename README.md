# 📚 BiblioFlow - Sistema de Gestión de Bibliotecas Universitarias

BiblioFlow es una solución multiplataforma orientada al diseño centrado en el usuario (DCU) que permite a los estudiantes buscar, reservar y consultar el estado de préstamos de libros en tiempo real mediante un código QR digital.

## 🚀 Arquitectura del Proyecto

* **Frontend Móvil:** .NET MAUI (C# / XAML)
* **Backend API:** ASP.NET Core Web API (.NET 8) con Entity Framework Core
* **Base de Datos:** PostgreSQL

## 🛠️ Estructura del Repositorio

```text
├── BiblioFlow.API/        # Proyecto de la API REST y modelos de Entity Framework
├── BiblioFlow.Mobile/     # Aplicación móvil .NET MAUI (Android / iOS / Windows)
├── Scripts_SQL/           # Scripts DDL e DML para la base de datos PostgreSQL
└── README.md              # Documentación del proyecto
