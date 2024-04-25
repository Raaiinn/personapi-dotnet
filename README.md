# personapi-dotnet

Repositorio para el laboratorio 1 de la asignatura Arquitectura de Software del programa de Ingenieria de Sistemas de la Pontificia Universidad Javeriana

Como prerequisitos para la realización de este laboratorio y su despliegue, se debe haber instalado con anterioridad las siguientes herramientas:
<ul>
  <li>SQL Server Express 2022</li>
  <li>SQL Server Management Studio SMSS</li>
  <li>Visual Studio Community 2022</li>
</ul>
Para un despliegue exitoso, descargue el codigo fuente de este repositorio o clonelo a través de Git.

Seguido, asegurese de estar ejecutando el servicio para el sistema gestor de base de datos SQL Server Express 2022 en su maquina local.

Una vez este desplegado el servicio, cree la base de datos a través de la herramienta SMSS con el Script en formato sql que se encuentra en el presente repositorio, bajo el nombre "script lab 1".

Cumplidos los pasos anteriores, inicie Visual Studio Community 2022 y agregue las respectivas cadenas de conexión al archivo Program.cs y ejecute.

Para acceder a la interfaz de pruebas de Swagger, ingrese a la ruta http://localhost:5252/swagger
