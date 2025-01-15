/*
Para hacer una conexion a una base de datos se hace de la siguiente manera
---- builder.Services.AddSqlServer<InformacionDeLaBaseDeDatos>("Data Source = (NombredelaBaseDeDatos); Initial Catalog = (NombreDeLaTabla); user id = (Usuario); password = (Contraseña)");
en user id y password es definido por la base de datos que tengamos en ese momento.
Las propiedades se separan por un punto y coma.

---builder.Services.AddDbContext<InformacionDeLaBaseDeDatos>(builder.Configuration.GetConnectionString("NombreDeLaConexion"));
En este caso se hace una conexion a una base de datos que se encuentra en el archivo appsettings.json, debido a que dejar la informacion de user y password en el codigo es inseguro.
Por eso SE RECOMIENDA dejarlo en el archivo appsettings.json y llamarlo desde ahi. 

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


Para indicarle la base de datos que es un primary key se usa [key]
Para indicarlela base de datos que es requerido se usa [Required]
Para indicarle a la base de datos que el campo tiene un tamaño maximo se usa [MaxLength(tamaño)]
Para indicarle a la base de datos que es una clave foranea se usa [ForeignKey("Nombre de la clave foranea")]
Para indicarle a la base de datos que no se mapee se usa [NotMapped]
Para indicarle a la base de datos que es un campo de solo lectura se usa [ReadOnly]
Para indicarle a la base de datos que es un campo de solo escritura se usa [WriteOnly]
Para indicarle a la base de datos que es un campo de solo lectura y escritura se usa [ReadWrite]
Para indicarle a la base de datos que es un campo que queremos que ignore de momento usamos [JsonIgnore]


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

FluentAPI 
Es una forma avanzada de configurar el modelo de datos en Entity Framework Core sin utilizar atributos de datos o anotaciones de datos.
ejemplo
builder.Entity<client>(entity=>
{
    entity.ToTable("Client");
    entity.HasKey(e=>e.PersonId);
    entity.Property(e=>e.FirstName).IsRequired().HasMaxLength(50);
    entity.Property(e=>e.LastName).IsRequired().HasMaxLength(50);
    entity.Property(e=>e.Email).IsRequired().HasMaxLength(50);
    entity.Property(e=>e.Phone).IsRequired().HasMaxLength(50);

    entity.HasOne(e=>e.Person)
    .WithOne(e=>e.Client)
    .HasForeignKey<Client>(e=>e.PersonId);
    .HasConstrainName("FK_Client_Person")
    .OnDelete(DeleteBehavior.SetNull);
});
ejemplo ir a Tareascontext.cs y mirar la a partir de la linea 12

Las dos formas son validas para hacer una conexion a una base de datos, pero la forma de FluentAPI es mas avanzada y se puede hacer mas cosas, ademas solo se debe de usar una de las dos
Esto se considera una buena practica.

.ToTable("NombreDeLaTabla") se usa para indicarle a la base de datos que el nombre de la tabla es diferente al nombre de la clase.
.HasKey(e=>e.NombreDeLaClave) se usa para indicarle a la base de datos que el campo es una clave primaria.
.HasOne(e=>e.NombreDeLaClase) se usa para indicarle a la base de datos que es una clave foranea.
.WithOne(e=>e.NombreDeLaClase) se usa para indicarle a la base de datos que es una clave foranea.
.HasForeignKey<NombreDeLaClase>(e=>e.NombreDeLaClave) se usa para indicarle a la base de datos que es una clave foranea.
.HasConstrainName("NombreDeLaClaveForanea") se usa para indicarle a la base de datos que es una clave foranea.
.OnDelete(DeleteBehavior.SetNull) se usa para indicarle a la base de datos que es una clave foranea.
.Ignore(e=>e.NombreDeLaClase) se usa para indicarle a la base de datos que no se mapee.
.IsRequired() se usa para indicarle a la base de datos que el campo es requerido. Se puede hacer false simplemente indicando false entre los parentesis.
.HasMaxLength(tamaño) se usa para indicarle a la base de datos que el campo tiene un tamaño maximo.


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Las reglas de normalizacion de las bases de datos dicen que las tablas deben de estar en singular y las columnas en plural.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
*** MIGRACIONES ***
COMANDOS BASICOS
dotnet ef migrations add **NombreDeLaMigracion**
dotnet ef migrations add MyMigration
dotnet ef database update.

Las migraciones deben de hacerse antes de que la base de datos tenga informacion.
Se guardara automaticamente la fecha en la que se hizo la migracion.
Los nombres de las migraciones deben de ser lo mas especificos posibles para entender cuales fueron los cambios

En C# se puede usar el metodo Guid.NewGUid() sin embargo los ID van a estar cambiando cada vez que EntityFramework haga la compracion entre el modelo actual y los cambios
que se han realizado, por lo cual no es el metodo mas recomendado para hacer una migracion. Por que lo toma como si fuera un nuevo cambio, por lo que se debe usar un ID 
que no cambie en el tiempo.

Siempre que hagamos un cambio dentro del modelo o de la configuracion de la base de datos se debe crear una migracion para llevar trazabilidad de los cambios que se han
Ido realizando.


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Para ingresar datos con Entity Framework usamos postman, con el atributo post en vez de get y le damos en body, raw y seleccionamos JSON, a continuacion ingresamos los datos
como se muestra en el ejemplo.

// Esto va en nuestro codigo
tabla.columnaconprimarykey = Guid.NewGuid();
tarea.segundacolumnaImportante = tipodedato
await dbcontext.addAsync(tabla);


//Esto va en PostMan
{
    "categoriaId" : "0e1c33da-c46f-4fa0-8c2f-18a2ad76e20f",
    "titulo" : "Terminar de ver pelicula en netflix",
    "descripcion" : null,
    "prioridadTarea": 2  
    }


Para actualizar un registro usando EntityFrameWork
esto va en nuestro codigo, Api/tareas/{id} es la url que vamos a usar en PostMan para encontrar el registro, TareasContext es donde esta la informacion de la tabla, 
Se mencionan la tabla a actualizar y se le da la ruta y como se va a buscar que es por ID.

Adicionalmente, se da una variable (var) donde almacenaremos la busqueda, haremos una comprobacion con el metodo If para asegurarnos de que el valor si exista
Y luego en las variables a actualizar pondremos los nuevos campos.

Por ultimo hacemos un return donde nos dara un error ne caso de no encontrar el valor que buscamos.

app.MapPut("/api/Tareas/{id}", async([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);

    if (tareaActual != null)
    {
        tareaActual.CategoriaId = tarea.CategoriaId;
        tareaActual.Titulo = tarea.Titulo;
        tareaActual.PrioridadTarea = tarea.PrioridadTarea;
        tareaActual.Descripcion = tarea.Descripcion;

        await dbContext.SaveChangesAsync();
        return Results.Ok();

    }
    return Results.NotFound();


En PostMan, usaremos el https que generamos y sera en PUT, luego en body al igual que cuando agregamos un dato y colocaremos los datos que queremos actualizar.


*/
