using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Models;
using P2_Desenv.Software.Data;
using P2_Desenv_Software.Models;

namespace P2_Desenv.Software.EndPoints
{
    public static class TreinoEndPoint
    {
        public static void MapTreinoEndPoint(this WebApplication app)
        {
            app.MapGet("/treinos", async (AppDbContext db) =>
            {
                try
                {
                    var treinos = await db.Treinos.ToListAsync();
                    return Results.Ok(treinos);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/treinos/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var treino = await db.Treinos.FindAsync(id);
                    if (treino == null)
                    {
                        return Results.NotFound();
                    }

                    return Results.Ok(treino);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/treinos", async (Treino treino, AppDbContext db) =>
            {
                try
                {
                    if (treino.AlunoId <= 0)
                    {
                        return Results.BadRequest("É necessário um aluno associado a um novo treino.");
                    }

                    if (treino.TreinadorId <= 0)
                    {
                        return Results.BadRequest("É necessário um treinador associado a um novo treino.");
                    }

                    db.Treinos.Add(treino);
                    await db.SaveChangesAsync();
                    return Results.Created($"/treinos/{treino.Id}", treino);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/treinos/{id}", async (int id, Treino updateTreino, AppDbContext db) =>
            {
                try
                {
                    var treino = await db.Treinos.FindAsync(id);
                    if (treino == null)
                    {
                        return Results.NotFound();
                    }

                    if (updateTreino.AlunoId == null)
                    {
                        return Results.BadRequest("Aluno é obrigatório.");
                    }

                    if (updateTreino.TreinadorId == null)
                    {
                        return Results.BadRequest("Treinador é obrigatório.");
                    }

                    if (!await db.Alunos.AnyAsync(a => a.Id == updateTreino.AlunoId))
                    {
                        return Results.BadRequest("Aluno não encontrado.");
                    }

                    treino.AlunoId = updateTreino.AlunoId;
                    treino.Aluno = updateTreino.Aluno;
                    treino.TreinadorId = updateTreino.TreinadorId;
                    treino.Treinador = updateTreino.Treinador;
                    treino.TreinoExercicios = updateTreino.TreinoExercicios;

                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/treinos/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var treino = await db.Treinos.Include(t => t.TreinoExercicios).FirstOrDefaultAsync(t => t.Id == id);
                    if (treino == null)
                    {
                        return Results.NotFound();
                    }

                    db.TreinoExercicios.RemoveRange(treino.TreinoExercicios);
                    db.Treinos.Remove(treino);

                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/alunos/{id}/treinos", async (int id, AppDbContext db) =>
            {
                try
                {
                    if (await db.Alunos.FindAsync(id) == null)
                    {
                        return Results.NotFound("Aluno não encontrado");
                    }

                    var treinos = await db.Treinos.Where(t => t.AlunoId == id).ToListAsync();
                    return Results.Ok(treinos);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/treinos/{id}/exercicios", async (int id, AppDbContext db) =>
            {
                try
                {
                    if (await db.Treinos.FindAsync(id) == null)
                    {
                        return Results.NotFound("Treino não encontrado");
                    }

                    var exercicios = await db.TreinoExercicios.Where(t => t.TreinoId == id).ToListAsync();
                    return Results.Ok(exercicios);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/treinos/{id}/exercicios", async (int id, TreinoExercicio treinoExercicio, AppDbContext db) =>
            {
                try
                {
                    var treino = await db.Treinos.FindAsync(id);
                    if (treino == null)
                    {
                        return Results.NotFound("Treino não encontrado");
                    }

                    if (treinoExercicio.TreinoId == null)
                    {
                        return Results.BadRequest("Treino não especificado ou não encontrado.");
                    }

                    if (treinoExercicio.ExercicioId == null)
                    {
                        return Results.BadRequest("Exercício não especificado ou não encontrado.");
                    }

                    if (treinoExercicio.Carga == null || treinoExercicio.Carga <= 0)
                    {
                        return Results.BadRequest("Carga do exercício inválida.");
                    }

                    if (treinoExercicio.QtdRepeticoes == null || treinoExercicio.QtdRepeticoes <= 0)
                    {
                        return Results.BadRequest("Quantidade de repetições do exercício inválida.");
                    }

                    if (treinoExercicio.QtdSeries == null || treinoExercicio.QtdSeries <= 0)
                    {
                        return Results.BadRequest("Quantidade de séries do exercício inválida.");
                    }

                    db.TreinoExercicios.Add(treinoExercicio);
                    await db.SaveChangesAsync();
                    return Results.Created($"/treinos/{treino.Id}/exercicios", treinoExercicio);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/treinos/{id}/exercicios/{exercicioId}", async (int id, int exercicioId, TreinoExercicio updateTreinoExercicio, AppDbContext db) =>
            {
                try
                {
                    var treino = await db.Treinos.FindAsync(id);
                    if (treino == null)
                    {
                        return Results.NotFound("Treino não encontrado");
                    }

                    var treinoExercicio = await db.TreinoExercicios.FindAsync(exercicioId);
                    if (treinoExercicio == null)
                    {
                        return Results.NotFound("Exercício do treino não encontrado");
                    }

                    if (updateTreinoExercicio.TreinoId == null)
                    {
                        return Results.BadRequest("Treino não especificado ou não encontrado.");
                    }

                    if (updateTreinoExercicio.ExercicioId == null)
                    {
                        return Results.BadRequest("Exercício não especificado ou não encontrado.");
                    }

                    if (updateTreinoExercicio.Carga == null || updateTreinoExercicio.Carga <= 0)
                    {
                        return Results.BadRequest("Carga do exercício inválida.");
                    }

                    if (updateTreinoExercicio.QtdRepeticoes == null || updateTreinoExercicio.QtdRepeticoes <= 0)
                    {
                        return Results.BadRequest("Quantidade de repetições do exercício inválida.");
                    }

                    if (updateTreinoExercicio.QtdSeries == null || updateTreinoExercicio.QtdSeries <= 0)
                    {
                        return Results.BadRequest("Quantidade de séries do exercício inválida.");
                    }

                    treinoExercicio.TreinoId = updateTreinoExercicio.TreinoId;
                    treinoExercicio.Treino = updateTreinoExercicio.Treino;
                    treinoExercicio.ExercicioId = updateTreinoExercicio.ExercicioId;
                    treinoExercicio.Exercicio = updateTreinoExercicio.Exercicio;
                    treinoExercicio.Carga = updateTreinoExercicio.Carga;
                    treinoExercicio.QtdRepeticoes = updateTreinoExercicio.QtdRepeticoes;
                    treinoExercicio.QtdSeries = updateTreinoExercicio.QtdSeries;

                    db.TreinoExercicios.Update(treinoExercicio);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/treinos/{id}/exercicios/{exercicioId}", async (int id, int exercicioId, AppDbContext db) =>
            {
                try
                {
                    var treino = await db.Treinos.FindAsync(id);
                    if (treino == null)
                    {
                        return Results.NotFound("Treino não encontrado");
                    }

                    var treinoExercicio = await db.TreinoExercicios.FindAsync(exercicioId);
                    if (treinoExercicio == null)
                    {
                        return Results.NotFound("Exercício do treino não encontrado");
                    }

                    db.TreinoExercicios.Remove(treinoExercicio);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}