using Microsoft.EntityFrameworkCore;
using P2_Desenv.Software.Models;
using P2_Desenv_Software.Models;

namespace P2_Desenv.Software.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos => Set<Aluno>();
        public DbSet<Mensalidade> Mensalidade => Set<Mensalidade>();
        public DbSet<Treinador> Treinador => Set<Treinador>();
        public DbSet<Treino> Treino => Set<Treino>();
        public DbSet<TreinoExercicio> TreinoExercicios => Set<TreinoExercicio>();
        public DbSet<Exercicio> Exercicios { get ; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreinoExercicio>()
                .HasKey(te => new { te.TreinoId, te.ExercicioId });
        }
    }
}