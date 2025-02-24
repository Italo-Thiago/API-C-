using Microsoft.EntityFrameworkCore;
using PersonAPI.Data;
using PersonAPI.Models;

namespace PersonAPI.Routes;

// static não precisa instanciar, e seus membros devem ser estáticos também
public static class PersonRoute
{
    // O tipo para rotas e WebApplication
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("person");

        route.MapPost(
            "",
            async (PersonRequest req, PersonContext context) =>
            {
                var person = new PersonModel(req.name);
                await context.AddAsync(person);
                await context.SaveChangesAsync();
            }
        );

        route.MapGet(
            "",
            async (PersonContext context) =>
            {
                var people = await context.People.ToListAsync();
                return Results.Ok(people);
            }
        );

        route.MapPut(
            "{id:guid}",
            async (Guid id, PersonRequest req, PersonContext context) =>
            {
                // var person = await context.People.FindAsync(id);
                var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                if (person == null)
                    return Results.NotFound();

                person.ChangeNome(req.name);
                await context.SaveChangesAsync();

                return Results.Ok(person);
            }
        );

        route.MapDelete(
            "{id:guid}",
            async (Guid id, PersonContext context) =>
            {
                var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                if (person == null)
                    return Results.NotFound();

                person.SetInvalidName();
                await context.SaveChangesAsync();

                return Results.Ok(person);
            }
        );
    }
}
