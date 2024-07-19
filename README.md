## PuntoVentaLaboratorio
Este Repositorio cuenta con dos proyectos: 
* Una web api Restful 
* Y una aplicacion de windows form.

# Pasos a seguir en web api:
- Restaurar los paquetes NuGet, dar click derecho sobre la solucion y seleccionar la opcion restaurar los paquetes NuGet.
  
- Crear la base de datos con todas sus tablas en SQL server managment Studio:
    - link de scripts ordenados de arriba hacia abajo: https://mega.nz/file/KQckxZbR#ael0CY54ZywTQLp8vwpuHlRWIbGQC5wot62eYA7i3Uc
 
- En el archivo appsettings.json, modificar la cadena de conexion "PuntoVentaDb" con los datos de su entorno local:
    - cadena de conexion: Data Source=DESKTOP-LB1UBLV;Initial Catalog=PuntoVenta;user id=sa;password=1234;TrustServerCertificate=true
    - cambiar:
       - Data source: por con el nombre del servidor local
       - user id: su usuario
       - password: su contrasena
  
- Mapear la base de datos con Scaffold-DBContext:
    - Modificar el siguiente comando: Scaffold-DBContext "Data Source=DESKTOP-LB1UBLV;Initial Catalog=PuntoVenta;user id=sa;password=1234;TrustServerCertificate=true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir DB/PuntoVentaEntities -context "PuntoVentaEntities" -f
      - nota: igualmente cambiar los datos de la cadena por los locales
      - cambiar:
         - Data source: por con el nombre del servidor local
         - user id: su usuario
         - password: su contrasena
      - notaImportante: los paquetes NuGet Microsoft.EntityFrameworkCore.SqlServer y Microsoft.EntityFrameworkCore.Tools  deben de estar instalados para que pueda correr el comando, por eso es importante restaurar los paquetes NuGet en el paso 1.
    - En la consola de paquetes correr el comando anterior

- Notas:
    * se recomienda arrancar el api como http

      
## Pasos a seguir en aplicacion Windows Form:
* En la raiz hay una carpeta llamada Enviroment, dentro de ella hay un archivo llamado Urls, en este archivo hay que cambiar la url del api que esta corriendo local, ya que la app Windows form se comunica con la web api.

* Proceso de inicio de sesion y autenticacion:
  - Credenciales:
    * Usuario: admin@mail.com
    * Contraseña: 12345
   
  - Iniciar sesión con las credenciales, si son correctas el api devuelve un objeto en el cual viene un token, dicho token se guarda en una variable (TOKEN) en una clase estatica llamada (Urls). asi que el token permanece en esa variable y se utiliza en todas las peticiones que se hagan a la api, ya que los endpoints estan protegidos y se necesita un token valido para acceder a ellos.
 


## Funcionalidad terminadas applicacion Windows Form:
* Login con autenticacion de usuario y password.
* La pantalla principal cuenta con un menu de opciones donde se encuentran los siguientes crud:
  - Crud Productos
  - Crud Clientes
  - Crud Usuarios

* La aplicacion hace consumo de la web api creada
    


  
