using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;
using proyectoef.models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(options =>options.UseInMemoryDatabase("TareasDB"));
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("ConexionTareas"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated();
    return Results.Ok($"Base de datos en memoria: {dbContext.Database.IsInMemory()}");
});

app.MapGet("/api/Tareas", async([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p => p.Categoria));

});
app.MapPost("/api/Tareas", async([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.FechaCreacion = DateTime.Now;
    //await dbContext.AddAsync(tarea);
    await dbContext.Tareas.AddAsync(tarea);

    await dbContext.SaveChangesAsync();

    return Results.Ok();

    //return Results.Ok(dbContext.Tareas.Where(p=> p.PrioridadTarea == proyectoef.models.Prioridad.Baja));

});
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

    // tarea.TareaId = Guid.NewGuid();
    // tarea.FechaCreacion = DateTime.Now;
    // //await dbContext.AddAsync(tarea);
    // await dbContext.Tareas.AddAsync(tarea);

    // await dbContext.SaveChangesAsync();

    // return Results.Ok();

    //return Results.Ok(dbContext.Tareas.Where(p=> p.PrioridadTarea == proyectoef.models.Prioridad.Baja));

});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext,[FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);
    if (tareaActual !=null)
    {
        dbContext.Remove(tareaActual);
        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }
    return Results.NotFound();

});

app.Run();


