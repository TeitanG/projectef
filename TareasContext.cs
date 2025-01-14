using Microsoft.EntityFrameworkCore;
using proyectoef.models;
namespace proyectoef;

public class TareasContext:DbContext
{
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> tareas {get;set;}
    public TareasContext(DbContextOptions<TareasContext> options):base(options){}       


    protected override void OnModelCreating(ModelBuilder ModelBuilder)
    {
        ModelBuilder.Entity<Categoria>(categoria =>
        {
            // asi se hace la configuracion de la tabla para denominarlo primarykey o hacer el [Key]
            categoria.ToTable("Categoria");
            categoria.HasKey(e => e.CategoriaId);

            categoria.Property(e=> e.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(e=> e.Descripcion);
            categoria.Property(e=> e.peso).IsRequired();
            

            
        });
        ModelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(e => e.TareaId);

            tarea.HasOne(e => e.Categoria).WithMany(e=>e.Tareas).HasForeignKey(e=>e.CategoriaId);
            tarea.Property(e => e.Titulo).IsRequired().HasMaxLength(150); 
            tarea.Property(e => e.Descripcion);
            tarea.Property(e => e.PrioridadTarea).IsRequired();
            tarea.Property(e => e.FechaCreacion); 
            tarea.Property(e => e.FechaVencimiento);

            tarea.Ignore(e => e.Resumen);    

        });
        
    }
    
    

}