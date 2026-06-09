using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Data;
using P2_Desenv.Software.Enums;
using P2_Desenv_Software.Models;

namespace P2_Desenv.Software.EndPoints
{
    public static class ExercicioEndPoints
    {
        public static void MapExercicioEndPoints(this WebApplication app)
        {
            app.MapGet("/exercicios", async (AppDbContext db) =>
            {
                try
                {
                    var exercicios = await db.Exercicios.ToListAsync();
                    return Results.Ok(exercicios);
                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao buscar exercícios.");
                }
            });



            app.MapGet("/exercicios/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var exercicio = await db.Exercicios.FindAsync(id);
                    return exercicio is not null ? Results.Ok(exercicio) : Results.NotFound();

                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao buscar exercício.");
                }
            });
                

            app.MapPost("/exercicios", async (Exercicio exercicio, AppDbContext db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(exercicio.Nome))
                    {
                        return Results.BadRequest("Nome é obrigatórios.");
                    }
                    else if (string.IsNullOrWhiteSpace(exercicio.GrupoMuscular))
                    {
                        return Results.BadRequest("Grupo muscular é obrigatório.");
                    }
                    else if (string.IsNullOrWhiteSpace(exercicio.Descricao))
                    {
                        return Results.BadRequest("Descrição é obrigatória.");
                    }
                    else if (!Enum.IsDefined(typeof(GrupoMuscular), exercicio.Grupo))
                    {
                        return Results.BadRequest("Grupo deve ser um valor válido.");
                    }
                    db.Exercicios.Add(exercicio);
                    await db.SaveChangesAsync();
                    return Results.Created($"/exercicios/{exercicio.Id}", exercicio);
                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao criar exercício.");
                }
            });

            app.MapPut("/exercicios/{id}", async (int id, Exercicio exercicio, AppDbContext db) =>
            {
                try
                {
                    var existente = await db.Exercicios.FindAsync(id);
                    if (existente is null) return Results.NotFound();
                    
                    else if (string.IsNullOrWhiteSpace(exercicio.Nome))
                    {
                        return Results.BadRequest("Nome é obrigatórios.");
                    }
                    else if (string.IsNullOrWhiteSpace(exercicio.GrupoMuscular))
                    {
                        return Results.BadRequest("Grupo muscular é obrigatório.");
                    }
                    else if (string.IsNullOrWhiteSpace(exercicio.Descricao))
                    {
                        return Results.BadRequest("Descrição é obrigatória.");
                    }
                    else if (!Enum.IsDefined(typeof(GrupoMuscular), exercicio.Grupo))
                    {
                        return Results.BadRequest("Grupo deve ser um valor válido.");
                    }

                    existente.Nome = exercicio.Nome;
                    existente.GrupoMuscular = exercicio.GrupoMuscular;
                    existente.Grupo = exercicio.Grupo;
                    existente.Descricao = exercicio.Descricao;

                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao atualizar exercício.");
                }
            });

            app.MapDelete("/exercicios/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    if (await db.Exercicios.FindAsync(id) is not Exercicio e) return Results.NotFound();
                    db.Exercicios.Remove(e);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem("Erro ao excluir exercício.");
                }
            });
        }
    }
}
