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
.IsRequired() se usa para indicarle a la base de datos que el campo es requerido.
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










*/
