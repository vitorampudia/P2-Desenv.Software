using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Data;
using P2_Desenv_Software.Models;

namespace P2_Desenv.Software.EndPoints
{
    public static class ExercicioEndPoints
    {
        public static void MapExercicioEndPoints(this WebApplication app)
        {
            app.MapGet("/exercicios", async (AppDbContext db) =>

                await db.Exercicios.ToListAsync());

            app.MapGet("/exercicios/{id}", async (int id, AppDbContext db) =>
                await db.Exercicios.FindAsync(id) is Exercicio e ? Results.Ok(e) : Results.NotFound());

            app.MapPost("/exercicios", async (Exercicio exercicio, AppDbContext db) =>
            {
                db.Exercicios.Add(exercicio);
                await db.SaveChangesAsync();
                return Results.Created($"/exercicios/{exercicio.Id}", exercicio);
            });

            app.MapPut("/exercicios/{id}", async (int id, Exercicio exercicio, AppDbContext db) =>
            {
                var existente = await db.Exercicios.FindAsync(id);
                if (existente is null) return Results.NotFound();

                existente.Nome = exercicio.Nome;
                existente.GrupoMuscular = exercicio.GrupoMuscular;
                existente.Grupo = exercicio.Grupo;
                existente.Descricao = exercicio.Descricao;

                await db.SaveChangesAsync();
                return Results.NoContent();
            });

            app.MapDelete("/exercicios/{id}", async (int id, AppDbContext db) =>
            {
                if (await db.Exercicios.FindAsync(id) is not Exercicio e) return Results.NotFound();
                db.Exercicios.Remove(e);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
