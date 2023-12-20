
# Tekton Technical Test 

Este proyecto es una aplicación desarrollada en C# utilizando la tecnología .Net 8 y Swagger como herramienta de documentación y testing. Además, se han empleado varios principios y patrones de diseño de software, entre ellos, REST, Clean Architecture, CQRS Pattern, Mediator Pattern, Repository Pattern, y se ha seguido la metodología de desarrollo TDD (Test-driven development).

La Arquitectura Limpia proporciona un diseño de software robusto y flexible que puede adaptarse fácilmente a los cambios y la mantenibilidad proporcionando una estructura modular con capas separadas para la API, la lógica de la aplicación, las entidades de dominio y la infraestructura.

El patrón CQRS (Command Query Responsibility Segregation) separa la aplicación en dos partes: comandos y consultas. Los comandos son responsables de modificar el estado de la aplicación, mientras que las consultas son responsables de recuperar datos de la aplicación. La separación de operaciones permite la escalabilidad y la optimización del rendimiento de la aplicación.

El proyecto incorpora marcos y bibliotecas populares como ASP.NET Core, Entity Framework Core, MediatR, AutoMapper, FluentValidation, Serilog y Swagger.


## Pre Requisitos

Para poder ejecutar este proyecto, es necesario tener instalado el SDK para .Net 8. También se requiere una conexión a PostgreSQL donde se va utilizar Entity Framework (EF) Migrations para trabajar con la base de datos.

## Instalación

  1. Clonar el repositorio:

  ```bash 
        git clone https://github.com/csdavila/Tekton.TechnicalTest.git
  ```

  2. Abrir el archivo de solución Tekton.TechnicalTest.sln en Visual Studio.

## Configuración

Antes de ejecutar el proyecto, es necesario configurar la conexión a la base de datos, para este caso se utilizó PostgreSQL. Para ello, se debe editar el archivo `appsettings.json` y modificar los parámetros de conexión según corresponda.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=nombreServidor;Port=puertoServidor;Database=nombreBaseDatos;User Id=nombreUsuario;Password=contraseña;"
  },
  ...
}
```

También es necesario configurar la ruta de Logs donde se guardará los archivos planos generados al utilizar la aplicación.

```json
{
  ...
  "LoggingPath": "C:\\Logs\\TektonTechnicalTest"
  ...
}
```

## Uso

Una vez configurada la conexión a la base de datos, se puede ejecutar el proyecto. Al iniciar la aplicación, se abrirá una ventana de Swagger donde se podrán ver y probar los diferentes endpoints disponibles.

Puede construir la solución presionado Ctrl + Shift + B o manualmente desde la opción Build -> Build Solution.

Puede correr la aplicación presionado F5 o manualmente desde la opción Debug -> Start Debugging.


## Estructura

El proyecto está estructurado siguiendo la Clean Code Architecture, lo que significa que se divide en diferentes capas según su función y responsabilidad.

- **Api**: Aquí se ubica los controladores y filtros de la aplicación, encargados de manejar las peticiones HTTP y coordinar las acciones correspondientes.

- **Application**: Aquí contiene los manejadores de las consultas y comandos definidos para usar el patrón CQRS. Los cuales son responsables de realizar las operaciones de negocio y acceder a los repositorios correspondientes.

- **Domain**: Aquí se encuentran las definiciones de los modelos de datos utilizados en la aplicación.

- **Infraestructure**: Aquí se encuentran los repositorios, encargados de interactuar con la base de datos y realizar operaciones de lectura y escritura. También se encuentran las migraciones generadas por EF Migrations, que permiten mantener y actualizar la estructura de la base de datos de forma automatizada.

- **UnitTests**: Aquí se encuentran los tests unitarios siguiendo el proceso de desarrollo TDD.
