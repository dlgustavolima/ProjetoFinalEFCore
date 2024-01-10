using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class JogosMapping : IEntityTypeConfiguration<Jogo>
    {
        public void Configure(EntityTypeBuilder<Jogo> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Descricao)
                .HasColumnType("varchar(1000)");

            builder.HasMany(p => p.JogoPlataforma)
                .WithOne(p => p.Jogo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.EstudioJogo)
                .WithOne(p => p.Jogo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.GeneroJogo)
                .WithOne(p => p.Jogo)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.CodigoAcesso)
                .WithMany(p => p.Jogos)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Jogos");
        }
    }
}
