using P2_Desenv.Software.Data;
using P2_Desenv.Software.Models;
using Microsoft.EntityFrameworkCore;
namespace P2_Desenv.Software.EndPoints
{
    public static class MensalidadeEndPoint
    {
        public static void MapMensalidadeEndPoints(this WebApplication app)
        {
           app.MapGet("/mensalidades", async (AppDbContext db) =>
            {
                try
                {
                    var mensalidades = await db.Mensalidades.ToListAsync();
                    return Results.Ok(mensalidades);
                } 
                catch (Exception ex) 
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapGet("/mensalidades/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var mensalidade = await db.Mensalidades.FindAsync(id);
                    if (mensalidade == null)
                    {
                        return Results.NotFound($"Mensalidade com ID {id} não encontrada.");
                    }
                    return Results.Ok(mensalidade);
                } 
                catch (Exception ex) 
                {
                    return Results.Problem(ex.Message);
                }
            });

            app.MapPost("/mensalidades", async (Mensalidade mensalidade, AppDbContext db) =>
            {
                try
                {
                    if (decimal.TryParse(mensalidade.Valor.ToString(), out decimal valor) && valor < 0)
                    {
                        return Results.BadRequest("Valor da mensalidade deve ser um número positivo.");
                    }
                    
                    else if (mensalidade.DataVencimento <= DateTime.Now) 
                    { 
                        return Results.BadRequest("Data de vencimento deve ser uma data futura.");
                    }

                    else if (mensalidade.DataPagamento <= DateTime.Now) 
                    { 
                        return Results.BadRequest("Data de pagamento deve ser uma data futura.");
                    }

                    else if (mensalidade.Status == 0) 
                    { 
                        return Results.BadRequest("Status da mensalidade deve ser definido.");
                    }

                    else if (mensalidade.AlunoId <= 0) 
                    { 
                        return Results.BadRequest("ID do aluno deve ser um número positivo.");
                    }

                    else if (mensalidade.TipoPagamento  <= 0) 
                    { 
                        return Results.BadRequest("Tipo de pagamento deve ser definido.");
                    }

                    db.Mensalidades.Add(mensalidade);
                    await db.SaveChangesAsync();
                    return Results.Created($"/mensalidades/{mensalidade.Id}", mensalidade);
                }
                catch (Exception ex) 
                {
                    return Results.Problem(ex.Message);
                }
            });
            app.MapPut("/mensalidades/{id}", async (int id, Mensalidade updateMensalidade, AppDbContext db) =>
            {
                try
                {
                    var mensalidade = await db.Mensalidades.FindAsync(id);
                    if (mensalidade == null)
                    {
                        return Results.NotFound($"Mensalidade com ID {id} não encontrada.");
                    }

                    if (decimal.TryParse(updateMensalidade.Valor.ToString(), out decimal valor) && valor < 0)
                    {
                        return Results.BadRequest("Valor da mensalidade deve ser um número positivo.");
                    }

                    else if (updateMensalidade.DataVencimento <= DateTime.Now)
                    {
                        return Results.BadRequest("Data de vencimento deve ser uma data futura.");
                    }

                    else if (updateMensalidade.DataPagamento <= DateTime.Now)
                    {
                        return Results.BadRequest("Data de pagamento deve ser uma data futura.");
                    }

                    else if (updateMensalidade.Status == 0)
                    {
                        return Results.BadRequest("Status da updateMensalidade deve ser definido.");
                    }

                    else if (updateMensalidade.AlunoId <= 0)
                    {
                        return Results.BadRequest("ID do aluno deve ser um número positivo.");
                    }

                    else if (updateMensalidade.TipoPagamento <= 0)
                    {
                        return Results.BadRequest("Tipo de pagamento deve ser definido.");
                    }

                    mensalidade.Valor = updateMensalidade.Valor;
                    mensalidade.DataVencimento = updateMensalidade.DataVencimento;
                    mensalidade.DataPagamento = updateMensalidade.DataPagamento;
                    mensalidade.Status = updateMensalidade.Status;
                    mensalidade.AlunoId = updateMensalidade.AlunoId;
                    mensalidade.TipoPagamento = updateMensalidade.TipoPagamento;

                    await db.SaveChangesAsync();
                    return Results.Ok(mensalidade);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
                
            });
            app.MapDelete("/mensalidades/{id}", async (int id, AppDbContext db) =>
            {
                try
                {
                    var mensalidade = await db.Mensalidades.FindAsync(id);
                    if (mensalidade == null)
                    {
                        return Results.NotFound($"Mensalidade com ID {id} não encontrada.");
                    }

                    db.Mensalidades.Remove(mensalidade);
                    await db.SaveChangesAsync();
                    return Results.Ok($"Mensalidade com ID {id} excluída com sucesso.");
                }
                catch(Exception ex) 
                {
                    return Results.Problem(ex.Message);
                }
            });

        }
    }
}
