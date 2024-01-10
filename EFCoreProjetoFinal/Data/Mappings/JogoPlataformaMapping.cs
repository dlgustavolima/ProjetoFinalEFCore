using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class JogoPlataformaMapping : IEntityTypeConfiguration<JogoPlataforma>
    {
        public void Configure(EntityTypeBuilder<JogoPlataforma> builder)
        {
            builder.ToTable("JogoPlataforma");

            builder.HasKey(p => new { p.JogosId, p.PlataformaId });

            builder.Property(p => p.JogosId)
                .IsRequired()
                .HasColumnName("JogosId");

            builder.Property(p => p.PlataformaId)
                .IsRequired()
                .HasColumnName("PlataformaId");

            builder.HasOne(p => p.Jogo)
                .WithMany(p => p.JogoPlataforma)
                .HasForeignKey(p => p.JogosId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Plataforma)
                .WithMany(c => c.JogoPlataforma)
                .HasForeignKey(c => c.PlataformaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
