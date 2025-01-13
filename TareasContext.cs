using Microsoft.EntityFrameworkCore;
using proyectoef.models;
namespace proyectoef;

public class TareasContext:DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> tareas {get;set;}
    public TareasContext(DbContextOptions<TareasContext> options):base(options){}       
    

}