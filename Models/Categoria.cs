using System.ComponentModel.DataAnnotations;

namespace proyectoef.models;

public class Categoria
{
    [Key]
    public Guid CategoriaId{get;set;}

    [Required]
    [MaxLength(150)]
    public required string Nombre{get;set;}
    public required string Descripcion{get;set;}
    public required virtual ICollection<Tarea> Tareas{get;set;}
}
/*
Para indicarle la base de datos que es un primary key se usa [key]
Para indicarlela base de datos que es requerido se usa [Required]
Para indicarle a la base de datos que el campo tiene un tamaño maximo se usa [MaxLength(tamaño)]
Para indicarle a la base de datos que es una clave foranea se usa [ForeignKey("Nombre de la clave foranea")]
Para indicarle a la base de datos que no se mapee se usa [NotMapped]
*/
