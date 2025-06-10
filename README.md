# dotnet-interview / TodoApi

Este proyecto es una implementación de la prueba técnica para la posición Jr AI Full Stack Developer en Crunchloop. Se trata de una API RESTful desarrollada en ASP.NET Core 8, utilizando una base de datos en memoria e integrando la funcionalidad requerida con MCP (Manifest of Capabilities Protocol) mediante Claude.

---

## 🛠️ Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (InMemoryDatabase)
- Claude (para ejecución de capacidades vía MCP)
- Swagger / OpenAPI
- Git & GitHub

---

## 🚀 Cómo ejecutar el proyecto

1. **Clonar el repositorio**

```bash
git clone https://github.com/FrancoVila/dotnet-interview.git
cd dotnet-interview

2. Abrir en Visual Studio 2022 o VS Code

3. Ejecutar la API

Desde consola:
dotnet run --project TodoApi

O presionando F5 desde Visual Studio con el proyecto TodoApi seleccionado como principal.

4. Acceder a Swagger

https://localhost:{puerto}/swagger
-Reemplazar {puerto} por el puerto local que se indique al correr.

5. Acceso externo vía Ngrok (para Claude)
Para permitir que Claude acceda a la API local, fue utilizado ngrok:

Instalar ngrok (si no lo tenés):

npm install -g ngrok
Exponer tu puerto local (reemplazá puerto por el que se muestra en consola al levantar la API):

ngrok http {puerto}
Usar la URL HTTPS pública generada por ngrok (por ejemplo https://abc123.ngrok.io) como endpoint para que Claude pueda consumir la API.

6. Testing
El proyecto incluye una carpeta TodoApi.Tests, pero no se realizaron modificaciones allí.

La API fue testeada manualmente a través de Swagger(manualmente) y Claude Desktop.

Todos los endpoints implementados fueron validados correctamente en entorno local.

7. Detalles de la implementación
Base de datos en memoria: no es necesario SQL Server ni DevContainers para correr el proyecto. Utiliza UseInMemoryDatabase de Entity Framework Core.

Integración MCP: se implementó la integración con Claude para que pueda ejecutar capacidades definidas vía el protocolo MCP.

Entrega lista para ejecutar: sin necesidad de configuración adicional.

Estructura del proyecto
dotnet-interview/
├── TodoApi/           -> Proyecto principal (API)
├── TodoApi.Tests/     -> Proyecto de tests
├── README.md          -> Instrucciones de uso
├── .github/           -> Workflows y configuración
└── dotnet-interview.sln


Autor
Franco Vila
https://github.com/FrancoVila

Notas
Proyecto fork del repositorio oficial de Crunchloop.

Se eliminaron instrucciones sobre DevContainers y uso de SQL Server para simplificar la ejecución local con una base de datos en memoria.

Se utilizó Ngrok para exponer localmente la API y permitir la ejecución de capacidades por parte de Claude.