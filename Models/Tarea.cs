using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace proyectoef.models;
public class Tarea
{
    //[Key]
    public Guid TareaId{get;set;}

    // [Required]
    // [MaxLength(30)]
    // [ForeignKey("CategoriaId")]
    public Guid CategoriaId{get;set;}
    
    // [Required]
    // [MaxLength(150)]
    public string Titulo {get;set;}
    public string Descripcion{get;set;}
    public Prioridad PrioridadTarea{get;set;}
    public DateTime FechaCreacion{get;set;}
    public virtual Categoria Categoria{get;set;} 

    public DateTime FechaVencimiento{get;set;}  

    [NotMapped]
    public string Resumen{get;set;}

} public enum Prioridad{Baja = 0, Media = 1, Alta = 2}