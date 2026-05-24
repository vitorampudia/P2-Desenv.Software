
namespace P2_Desenv.Software
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi(); // Gera o JSON do .NET em: /openapi/v1.json

                // Configura a interface antiga para ler o JSON novo
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/openapi/v1.json", "Minha API v1");
                });
            }

            app.UseAuthorization();

            app.Run();
        }
    }
}
