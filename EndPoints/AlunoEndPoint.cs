using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Models;
using P2_Desenv.Software.Data;

namespace P2_Desenv.Software.EndPoints
{
    public static class AlunoEndPoint
    {
        public static void MapAlunoEndPoints(this WebApplication app)
        {
            app.MapGet("/alunos", async (AppDbContext db) =>
            {
                try
                {
                    var alunos = await db.Alunos.ToListAsync();
                    return Results.Ok(alunos);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/alunos/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var aluno = await db.Alunos.FindAsync(id);
                    if (aluno == null)
                    {
                        return Results.NotFound();
                    }

                    return Results.Ok(aluno);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/alunos", async (Aluno aluno, AppDbContext db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(aluno.Nome) || string.IsNullOrWhiteSpace(aluno.Cpf))
                    {
                        return Results.BadRequest("Nome e CPF são obrigatórios.");
                    }

                    if (await db.Alunos.AnyAsync(a => a.Cpf == aluno.Cpf))
                    {
                        return Results.BadRequest("Já existe um aluno com este CPF.");
                    }

                    if (aluno.DataNascimento >= DateOnly.FromDateTime(DateTime.Today))
                    {
                        return Results.BadRequest("Data de nascimento inválida.");
                    }

                    if (aluno.Altura <= 0)
                    {
                        return Results.BadRequest("Altura é obrigatória e deve ser maior que zero.");
                    }

                    if (aluno.Peso <= 0)
                    {
                        return Results.BadRequest("Peso é obrigatório e deve ser maior que zero.");
                    }

                    db.Alunos.Add(aluno);
                    await db.SaveChangesAsync();
                    return Results.Created($"/alunos/{aluno.Id}", aluno);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/alunos/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var aluno = await db.Alunos.FindAsync(id);
                    if (aluno == null)
                    {
                        return Results.NotFound();
                    }
                    db.Alunos.Remove(aluno);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/alunos/{id}", async (int id, Aluno updateAluno, AppDbContext db) =>
            {
                try
                {
                    var aluno = await db.Alunos.FindAsync(id);
                    if (aluno == null)
                    {
                        return Results.NotFound();
                    }

                    if (string.IsNullOrWhiteSpace(updateAluno.Nome))
                    {
                        return Results.BadRequest("Nome é obrigatório.");
                    }

                    if (string.IsNullOrWhiteSpace(updateAluno.Cpf))
                    {
                        return Results.BadRequest("CPF é obrigatório.");
                    }

                    if (await db.Alunos.AnyAsync(a => a.Cpf == updateAluno.Cpf && a.Id != id))
                    {
                        return Results.BadRequest("Já existe um aluno com este CPF.");
                    }

                    if (updateAluno.DataNascimento >= DateOnly.FromDateTime(DateTime.Today))
                    {
                        return Results.BadRequest("Data de nascimento inválida.");
                    }

                    if (updateAluno.Altura <= 0)
                    {
                        return Results.BadRequest("Altura é obrigatória e deve ser maior que zero.");
                    }

                    if (updateAluno.Peso <= 0)
                    {
                        return Results.BadRequest("Peso é obrigatório e deve ser maior que zero.");
                    }

                    aluno.Nome = updateAluno.Nome;
                    aluno.Cpf = updateAluno.Cpf;
                    aluno.DataNascimento = updateAluno.DataNascimento;
                    aluno.Altura = updateAluno.Altura;
                    aluno.Peso = updateAluno.Peso;
                    aluno.TreinadorId = updateAluno.TreinadorId;

                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
;            });
        }
    }
}
