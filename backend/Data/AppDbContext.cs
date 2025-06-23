using backend.Model;
using backend.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions options) : base(options) { }



        public DbSet<Chamado> tb_chamado { get; set; }
        public DbSet<ChamadoAcompanhamento> tb_chamado_acompanhamento { get; set; }
        public DbSet<ChamadoAtendimento> tb_chamado_atendimento { get; set; }
        public DbSet<Equipamento> tb_equipamento { get; set; }
        public DbSet<Estabelecimento> tb_estabelecimento { get; set; }
        public DbSet<Setor> tb_setor { get; set; }
        public DbSet<Usuario> tb_usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresEnum<GrupoSuporte>();
            modelBuilder.HasPostgresEnum<StatusAtendimento>();
            modelBuilder.HasPostgresEnum<TipoUsuario>();

            modelBuilder.Entity<Chamado>(entity =>
            {
                entity.HasOne(e => e.Equipamento)
                    .WithMany()
                    .HasForeignKey(e => e.EquipamentoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SetorDestino)
                    .WithMany()
                    .HasForeignKey(e => e.SetorDestinoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SetorSolicitante)
                    .WithMany()
                    .HasForeignKey(e => e.SetorSolicitanteId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Estabelecimento)
                    .WithMany()
                    .HasForeignKey(e => e.EstabelecimentoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ChamadoAcompanhamento>(entity =>
            {
                entity.HasOne(e => e.Chamado)
                    .WithMany(c => c.Acompanhamentos)
                    .HasForeignKey(e => e.ChamadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Usuario)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ChamadoAtendimento>(entity =>
            {
                entity.HasOne(e => e.Chamado)
                    .WithMany()
                    .HasForeignKey(e => e.ChamadoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.UsuarioAtendimento)
                    .WithMany()
                    .HasForeignKey(e => e.UsuarioAtendimentoId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SetorAtual)
                    .WithMany()
                    .HasForeignKey(e => e.SetorAtualId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SetorTransferencia)
                    .WithMany()
                    .HasForeignKey(e => e.SetorTransferenciaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Equipamento>(entity =>
            {
                entity.HasOne(e => e.Setor)
                    .WithMany()
                    .HasForeignKey(e => e.SetorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Setor>(entity =>
            {
                entity.HasOne(e => e.Estabelecimento)
                    .WithMany()
                    .HasForeignKey(e => e.EstabelecimentoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

    }
}