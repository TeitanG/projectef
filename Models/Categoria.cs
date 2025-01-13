using System.ComponentModel.DataAnnotations;

namespace proyectoef.models;

public class Categoria
{
    //[Key]
    public Guid CategoriaId{get;set;}

    //[Required]
    //[MaxLength(150)]
    public required string Nombre{get;set;}
    public required string Descripcion{get;set;}
    public required virtual ICollection<Tarea> Tareas{get;set;}
}

