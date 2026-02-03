<h1 align="center">POKEMON API MINI-GAME 😼</h1>

<p align="center">
  <img src="https://raw.githubusercontent.com/robertodfj/pokemon/refs/heads/main/squirtle-squirtle-squad.gif?token=GHSAT0AAAAAADONMK7AYYPO2EZBXMNDJCGW2MA6ZHQ" alt="Gif de un pokemon">
</p>

<p align="center">
API REST desarrollada en C# con .NET 7 para gestionar usuarios y Pokémon. Permite registro, login, captura, listado y liberación de Pokémon de manera segura usando JWT y middleware de manejo de errores.
</p>

## 🔹 Tecnologías utilizadas
*	Lenguaje: C#
*	Framework: .NET 7 (ASP.NET Core)
*	Base de datos: SQLite con Entity Framework Core	
*	Autenticación: JWT (JSON Web Tokens)
*	Pruebas: Postman
*	HTTP Cliente: HttpClient para consumo de la PokeAPI￼
*	Manejo de errores: Middleware personalizado (ExceptionMiddleware)

## 🔹 Estructura del proyecto

| Carpera / Archivos | Descripción|
| ------------- |:-------------:|
| Program.cs  | Configuración principal de la aplicación, DI, JWT, middleware y rutas.
| data/     | Contexto de base de datos (AppDBContext), migraciones y SeedData para usuarios iniciales.
| dto/      | Data Transfer Objects para validar y enviar datos de usuarios y Pokémon.
| middleware/ | Middleware de manejo de errores y excepciones personalizadas.
| service/ | Lógica de negocio: AuthService y PokemonService.
| token/ | Generación y validación de tokens JWT (GenerateToken).
| model/| Modelos de base de datos (User, Pokemon).

## 🔹 Funcionalidades
### 1️⃣ Autenticación de usuarios

##### Registro de usuario
POST /auth/register
+ Request body:
```
{
  "email": "usuario1@example.com",
  "password": "SuperSecreta123!",
  "confirmPassword": "SuperSecreta123!"
}
```

* ✅ Usuario registrado correctamente
* ❌ Error 409 si el email ya existe	
* ❌ Error 400 si las contraseñas no coinciden

Registro de admin
* 🔐 Solo un admin puede registrar a otro admin.
* 🔏 Se requiere token de admin para realizar la acción.
#### Login
POST /auth/login
+ Request body:
```
{
  "email": "usuario1@example.com",
  "password": "SuperSecreta123!"
}
```

* ✅ Devuelve token JWT válido
* ❌ Error 401 si el usuario o contraseña son incorrectos

### 2️⃣ Gestión de Pokémon

##### Captura de Pokémon
POST /pokemon/capture
* Valida si el usuario ya tiene el Pokémon → error 409
* Algoritmo de captura basado en nivel → error 400 si falla
* Llama a la PokeAPI para obtener datos y guarda el Pokémon en DB

##### Ver Pokémon del usuario
GET /pokemon
* Devuelve lista de Pokémon del usuario autenticado
* 401 si el token es inválido o ha expirado

##### Liberar Pokémon
DELETE /pokemon/release/{pokemonID}
* Antes solo devolvía true/false
* Ahora devuelve un DTO completo del Pokémon eliminado:
```
{
  "id": 2,
  "name": "ivysaur",
  "category": "Seed Pokémon",
  "imageURL": "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/shiny/2.png",
  "isShiny": true,
  "level": 60,
  "ownerId": 2
}
```
* ❌ Si el Pokémon no existe para ese usuario → error 404

### 3️⃣ Manejo de errores
* Middleware global ExceptionMiddleware captura:
* Excepciones personalizadas: ConflictException, BadRequestException, NotFoundException
* Errores inesperados: Devuelve 500 con mensaje genérico
* JWT sin token → 401 con mensaje:

```
{
  "message": "Authentication token is missing or invalid"
}
```
## 🐳 Docker

La API se puede ejecutar dentro de un contenedor Docker para facilitar el despliegue y la portabilidad.
#### 1️⃣ Dockerfile
```
# Stage 1: build
FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src


COPY *.csproj ./
RUN dotnet restore 

COPY . .
RUN dotnet publish "Pokemon.csproj" -c Release -o /app/publish

# Stage 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5039
ENTRYPOINT ["dotnet", "Pokemon.dll"]
```
#### 2️⃣ Utiliza mi imagen de docker
```
docker pull robertodfj/pokemon.api:latest
docker run -p 5039:8080 robertodfj/pokemon.api
```

## 🔹 Pruebas realizadas

Se han realizado todas las pruebas de manera manual con Postman, incluyendo:
* Registro y login de usuario y admin
* Captura de Pokémon con validación de nivel y duplicados
* Listado de Pokémon del usuario
* Liberación de Pokémon (devolviendo DTO completo)
* Validación de errores: 400, 401, 404, 409

Colección Postman compartida:
[Postman Workspace - Pokémon API￼ 🐦‍🔥](    https://robertodefrutos.postman.co/workspace/Personal-Workspace~54d28531-8963-4457-a4f4-1c5c4fac03ec/collection/45775665-1a6712e5-0fcd-4118-bc7d-912e5e7b2b21?action=share&creator=45775665).


## ⚡ Observaciones finales
+ Código modular y limpio: DTOs, servicios y middleware separados
+ JWT + roles correctamente implementados
+ Uso de HttpClient para consumir APIs externas
+ Validaciones correctas y robustas para producción 

+ El proyecto está `listo para` ser extendido a frontend en ` React o Angular `

## Creador y licencia

Creado por `Roberto de Frutos Jiménez` sin licencia, con fines educativos.
