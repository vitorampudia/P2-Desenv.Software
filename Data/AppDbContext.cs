using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Models;

namespace P2_Desenv.Software.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) 
        : DbContext(options)
    {
        public DbSet<Aluno> Alunos => Set<Aluno>();
        public DbSet<Mensalidade> Mensalidade => Set<Mensalidade>();
        public DbSet<Treinador> Treinador => Set<Treinador>();
        public DbSet<Treino> Treino => Set<Treino>();
        public DbSet<Exercicio> Exercicio => Set<Exercicio>();
    }
}
