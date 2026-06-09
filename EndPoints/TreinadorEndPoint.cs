using P2_Desenv.Software.Models;
using P2_Desenv.Software.Data;
using Microsoft.EntityFrameworkCore;
namespace P2_Desenv.Software.EndPoints
{
    public static class TreinadorEndPoint
    {
        public static void MapTreinadorEndPoints(this WebApplication app)
        {
            app.MapGet("/treinadores", async (AppDbContext db) =>
            {
                try
                {
                    var treinadores = await db.Treinador.ToListAsync();
                    return Results.Ok(treinadores);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/treinadores/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var treinador = await db.Treinador.FindAsync(id);
                    if (treinador == null)
                    {
                        return Results.NotFound();
                    }
                    return Results.Ok(treinador);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/treinadores", async (Treinador treinador, AppDbContext db) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(treinador.Nome) || string.IsNullOrWhiteSpace(treinador.Cref) || string.IsNullOrWhiteSpace(treinador.Especializacao))
                {
                    return Results.BadRequest("Nome, CREF e Especialização são obrigatórios.");
                }

                if (await db.Treinador.AnyAsync(t => t.Cref == treinador.Cref))
                {
                    return Results.BadRequest("Já existe um treinador com este CREF.");
                }

                db.Treinador.Add(treinador);
                await db.SaveChangesAsync();
                return Results.Created($"/treinadores/{treinador.Id}", treinador);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapDelete("/treinadores/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var treinador = await db.Treinador.FindAsync(id);
                    if (treinador == null)
                    {
                        return Results.NotFound();
                    }
                    db.Treinador.Remove(treinador);
                    await db.SaveChangesAsync();
                    return Results.NoContent();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPut("/treinadores/{id}", async (int id, Treinador updatedTreinador, AppDbContext db) =>
            {
                try
                {
                    var treinador = await db.Treinador.FindAsync(id);
                    if (treinador == null)
                    {
                        return Results.NotFound();
                    }
                    if (string.IsNullOrWhiteSpace(updatedTreinador.Nome) || string.IsNullOrWhiteSpace(updatedTreinador.Cref) || string.IsNullOrWhiteSpace(updatedTreinador.Especializacao))
                    {
                        return Results.BadRequest("Nome, CREF e Especialização são obrigatórios.");
                    }
                    if (await db.Treinador.AnyAsync(t => t.Cref == updatedTreinador.Cref && t.Id != id))
                    {
                        return Results.BadRequest("Já existe um treinador com este CREF.");
                    }
                    treinador.Nome = updatedTreinador.Nome;
                    treinador.Cref = updatedTreinador.Cref;
                    treinador.Especializacao = updatedTreinador.Especializacao;
                    await db.SaveChangesAsync();
                    return Results.Ok(treinador);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            });
        }
    }
}
