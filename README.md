# PruebaApp API

PruebaApp es una API RESTful desarrollada en **ASP.NET Core** para gestionar **clientes**, **productos**, **pedidos** y **Pedido productos**. 

Este proyecto incluye autenticación basada en JWT, una arquitectura modular y soporte para ejecución en contenedores Docker.

---

## Características principales

- CRUD completo para clientes, productos, pedidos y PedidoProductos.
- Autenticación y autorización basada en **JWT** con roles (`Admin` y `Cliente`).
- Usuarios `admin` y `cliente` para obtener token:
Username = "admin", Password = "password123", Role = "Admin"
Username = "cliente", Password = "password123", Role = "Cliente"
- Configuración de base de datos MySQL.
- Despliegue listo para Docker con `docker-compose`.

---

## Requisitos previos

Asegúrate de tener instalados:

- **Docker**
- **.NET 8.0 SDK** (si deseas trabajar localmente).
- **Postman** o cualquier herramienta para probar APIs.

---

## Configuración rápida

### 1. Clona el repositorio

ejecuta lo siguiente en un cmd 
git clone <https://github.com/jobs2306/PruebaCompusLands.git>

Dirigirse a la carpeta del proyecto
cd PruebaApp

#### 2. Configura las variables de entorno
El archivo docker-compose.yml ya tiene configuradas las credenciales por defecto para la base de datos. Puedes ajustarlas si es necesario:

yaml
Copy code
environment:
  MYSQL_ROOT_PASSWORD: rootpassword
  MYSQL_DATABASE: dbpruebanet
  MYSQL_USER: user
  MYSQL_PASSWORD: userpassword

Si prefieres ejecutar localmente, ajusta appsettings.json:

json
Copy code
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=dbpruebanet;user=user;password=userpassword"
}

#### 3. Construir y ejecutar (Para este caso con Docker)
Desde la raíz del proyecto, ejecuta:

docker-compose up --build

Esto levantará la aplicación y la base de datos MySQL en contenedores.

Por defecto el archivo appsettings.json está configurado para usarse desde el contenedor "DefaultConnection": "server=mysql;port=3306;database=dbpruebanet;user=user;password=userpassword"

### Migraciones
En caso de querer hacer una migración nueva debe cambiarlo a "DefaultConnection": server=localhost;port=3307;database=dbpruebanet;user=user;password=userpassword
Esto le permitirá conectarse, tener en cuenta que este cambio se debe realizar despues de haber hecho docker-compose up --build

Para iniciar la migracion ejecute
dotnet ef add migrations NameMigration
dotnet ef database update

Esto le creará la migración a la base de datos del contenedor

#### Uso de la API
Tener en cuenta que debe usar Postman para poder realizar las pruebas que se indican a continuación.
La URL raiz es http://localhost:8080 , a esto le agrega las rutas de las api's

Autenticación
Obtén un token JWT en el endpoint de login:

Endpoint: POST /api/auth/login

Body:

json
Copy code
{
    "username": "admin",
    "password": "password123"
}
(tambien puede usar el de cliente)

Usa el token en los headers para los demás endpoints:

Authorization: Bearer <tu_token> 

### Endpoints disponibles
    AuthController
Prueba de endpoint: GET /api/auth/Prueba
Login y generación de token JWT: POST /api/auth/login

    ClientesController
Crear cliente: POST /api/clientes
Obtener todos los clientes: GET /api/clientes
Obtener cliente por ID: GET /api/clientes/{id}
Actualizar cliente: PUT /api/clientes/{id}
Eliminar cliente: DELETE /api/clientes/{id}
    
    PedidoController
Crear pedido: POST /api/pedidos
Obtener todos los pedidos: GET /api/pedidos
Obtener pedido por ID: GET /api/pedidos/{id}
Actualizar pedido: PUT /api/pedidos/{id}
Eliminar pedido: DELETE /api/pedidos/{id}
    
    PedidoProductosController
Crear relación pedido-producto: POST /api/pedidoproductos
Obtener todas las relaciones pedido-producto: GET /api/pedidoproductos
Obtener relación pedido-producto por ID: GET /api/pedidoproductos/{id}
Actualizar relación pedido-producto: PUT /api/pedidoproductos/{id}
Eliminar relación pedido-producto: DELETE /api/pedidoproductos/{id}
    
    ProductoController
Crear producto: POST /api/productos
Obtener todos los productos (con filtros opcionales): GET /api/productos
Obtener producto por ID: GET /api/productos/{id}
Actualizar producto: PUT /api/productos/{id}
Eliminar producto: DELETE /api/productos/{id}




