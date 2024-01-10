using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class GeneroJogoMapping : IEntityTypeConfiguration<GeneroJogo>
    {
        public void Configure(EntityTypeBuilder<GeneroJogo> builder)
        {
            builder.ToTable("GeneroJogo");

            builder.HasKey(p => new { p.JogosId, p.GeneroId });

            builder.Property(p => p.JogosId)
                .IsRequired()
                .HasColumnName("JogosId");

            builder.Property(p => p.GeneroId)
                .IsRequired()
                .HasColumnName("GeneroId");

            builder.HasOne(p => p.Jogo)
                .WithMany(p => p.GeneroJogo)
                .HasForeignKey(p => p.JogosId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Genero)
                .WithMany(c => c.GeneroJogo)
                .HasForeignKey(c => c.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
