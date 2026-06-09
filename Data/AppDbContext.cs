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
        public DbSet<Mensalidade> Mensalidades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TreinoExercicio>()
                .HasKey(te => new { te.TreinoId, te.ExercicioId });
            
            modelBuilder.Entity<Aluno>()
                .HasIndex(a => a.Cpf)
                .IsUnique();

            modelBuilder.Entity<Treinador>()
                .HasIndex(t => t.Cref)
                .IsUnique();

            modelBuilder.Entity<Aluno>()
                .HasOne(a => a.Treinador)
                .WithMany(t => t.Alunos)
                .HasForeignKey(a => a.TreinadorId);

            modelBuilder.Entity<TreinoExercicio>()
                .HasKey(te => new { te.TreinoId, te.ExercicioId });

            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.Treino)
                .WithMany(t => t.TreinoExercicios)
                .HasForeignKey(te => te.TreinoId);

            modelBuilder.Entity<TreinoExercicio>()
                .HasOne(te => te.Exercicio)
                .WithMany(e => e.TreinoExercicios)
                .HasForeignKey(te => te.ExercicioId);

            modelBuilder.Entity<Mensalidade>()
                .HasOne(m => m.Aluno)
                .WithMany(a => a.Mensalidades)
                .HasForeignKey(m => m.AlunoId);
        }
    }
}