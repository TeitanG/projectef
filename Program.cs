using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoef;

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
    return Results.Ok(dbContext.Tareas.Where(p=> p.PrioridadTarea == proyectoef.models.Prioridad.Baja));;

});

app.Run();


