/*
Para hacer una conexion a una base de datos se hace de la siguiente manera
---- builder.Services.AddSqlServer<InformacionDeLaBaseDeDatos>("Data Source = (NombredelaBaseDeDatos); Initial Catalog = (NombreDeLaTabla); user id = (Usuario); password = (Contraseña)");
en user id y password es definido por la base de datos que tengamos en ese momento.
Las propiedades se separan por un punto y coma.

---builder.Services.AddDbContext<InformacionDeLaBaseDeDatos>(builder.Configuration.GetConnectionString("NombreDeLaConexion"));
En este caso se hace una conexion a una base de datos que se encuentra en el archivo appsettings.json, debido a que dejar la informacion de user y password en el codigo es inseguro.
Por eso SE RECOMIENDA dejarlo en el archivo appsettings.json y llamarlo desde ahi. 


Para indicarle la base de datos que es un primary key se usa [key]
Para indicarlela base de datos que es requerido se usa [Required]
Para indicarle a la base de datos que el campo tiene un tamaño maximo se usa [MaxLength(tamaño)]
Para indicarle a la base de datos que es una clave foranea se usa [ForeignKey("Nombre de la clave foranea")]
Para indicarle a la base de datos que no se mapee se usa [NotMapped]
Para indicarle a la base de datos que es un campo de solo lectura se usa [ReadOnly]
Para indicarle a la base de datos que es un campo de solo escritura se usa [WriteOnly]
Para indicarle a la base de datos que es un campo de solo lectura y escritura se usa [ReadWrite]




*/
