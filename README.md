### PuntoVentaLaboratorio
Este Repositorio cuenta con dos proyectos: 
* Una web api Restful
    - Tecnologias usadas:
       - .Net 8.0
       - Lenguaje C#
* Y una aplicacion de windows form.
    - Tecnologias usadas:
      - .Net Framework 4.7.2
      - Lenguaje C#
        
## Herramientas usadas
* Visual studio 2022
* SQL Server Managment Studio 20
  
## Pasos Generales
* Clonar el repositorio:
  - git clone https://github.com/jaimegh2012/PuntoVentaLaboratorio
* Una vez clonado, abrir el proyecto en Visual Studio 2022

  Nota: al arrancar los proyectos, arrancar primero el api, luego la applicacion windows form como una nueva instancia.

## Pasos a seguir en web api:
- Restaurar los paquetes NuGet, dar click derecho sobre la solucion y seleccionar la opcion restaurar los paquetes NuGet.
  
- Crear la base de datos con todas sus tablas en SQL server managment Studio:
    - link de scripts ordenados de arriba hacia abajo: https://mega.nz/file/2QtnFZbS#ubSptdXihBa8DhiwnBNhCtnNcQvxSzjC3RraCXv9RjM
 
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
      - Nota importante: no debe haber ningun error de depuracion para poder correr el comando, si existe algun error hacer lo siguiente:
        * comentar las lineas donde estan los errores o excluir del proyecto las carpetas que contienen los archivos con errores y despues cuando ya se haya mapeado la db incluirlas de nuevo en el proyecto. 
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
 
  - Nota: la contraseña se envia crifrada, se utiliza el algoritmo AES.
 


## Funcionalidad terminadas applicacion Windows Form:
* Login con autenticacion de usuario y password.
* La pantalla principal cuenta con un menu de opciones donde se encuentran los siguientes crud:
  - Crud Productos
  - Crud Clientes
  - Crud Direccines de Clientes
  - Crud Usuarios
  * Nota: todos estos crud tienen tres botones:
      - Guardar: Crea o actualiza un registro.
      - Limpiar: Limpia los inputs, para crear un nuevo registro;
      - Salir: Cierra el form
* Pasos Crear un registro:
    - si los campos estan llenos, dar click en el boton limpiar y rellenar los campos con los datos del nuevo registro, luego en guardar;
* Pasos Actualizar un registro:
    - En el datagridView seleccionar el registro a modificar, luego modificar los campos necesarios, luego guardar.

* La aplicacion hace consumo de la web api creada
    


  
