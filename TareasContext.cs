using Microsoft.EntityFrameworkCore;
using proyectoef.models;
namespace proyectoef;

public class TareasContext:DbContext
{
    
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}
    public TareasContext(DbContextOptions<TareasContext> options):base(options){}       


    protected override void OnModelCreating(ModelBuilder ModelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria()
        {
            CategoriaId = Guid.Parse("0e1c33da-c46f-4fa0-8c2f-18a2ad76e20f"), 
            Nombre = "Actividades Pendientes",             
            peso = 20,
            Descripcion = "Descripcion pendiente",
            Tareas = new List<Tarea>()
        });
        categoriasInit.Add(new Categoria()
        {
            CategoriaId = Guid.Parse("b2c36661-97d0-4fa7-8982-ee49db042e93"), 
            Nombre = "Actividades Personales",            
            peso = 50,
            Descripcion = "Descripcion pendiente",
            Tareas = new List<Tarea>()
        });


        ModelBuilder.Entity<Categoria>(categoria =>
        {
            // asi se hace la configuracion de la tabla para denominarlo primarykey o hacer el [Key]
            categoria.ToTable("Categoria");
            categoria.HasKey(e => e.CategoriaId);

            categoria.Property(e=> e.Nombre).IsRequired().HasMaxLength(150);
            categoria.Property(e=> e.Descripcion).IsRequired(false);
            categoria.Property(e=> e.peso).IsRequired();
            categoria.HasData(categoriasInit);     
        });

         List<Tarea> tareasInit = new List<Tarea>();
        tareasInit.Add(new Tarea()
        {
            TareaId = Guid.Parse("33e3d92c-49f9-45d7-95fe-d0847f5af412"), 
            CategoriaId = Guid.Parse("0e1c33da-c46f-4fa0-8c2f-18a2ad76e20f"), 
            Titulo = "Pago De Servicios Publicos",             
            PrioridadTarea = Prioridad.Media, 
            Descripcion = "Descripcion pendiente",
            FechaCreacion = DateTime.Now, 
            FechaVencimiento = DateTime.Now.AddDays(10)
        });
        tareasInit.Add(new Tarea()
        {
            TareaId = Guid.Parse("159778e5-8a82-4db1-9fd4-e64c2eb01e19"), 
            CategoriaId = Guid.Parse("b2c36661-97d0-4fa7-8982-ee49db042e93"), 
            Titulo = "Terminar Cursos Platzi",             
            PrioridadTarea = Prioridad.Alta, 
            Descripcion = "Descripcion pendiente",
            FechaCreacion = new DateTime(2025,01,14), 
            FechaVencimiento = new DateTime(2025,01,18)
        });


        ModelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(e => e.TareaId);

            tarea.HasOne(e => e.Categoria).WithMany(e=>e.Tareas).HasForeignKey(e=>e.CategoriaId);
            tarea.Property(e => e.Titulo).IsRequired().HasMaxLength(500); 
            tarea.Property(e => e.Descripcion).IsRequired(false);
            tarea.Property(e => e.PrioridadTarea); 
            tarea.Property(e => e.FechaCreacion); 
            tarea.Property(e => e.FechaVencimiento);
            tarea.HasData(tareasInit);

            tarea.Ignore(e => e.Resumen);    

        });
        
    }
    
    

}