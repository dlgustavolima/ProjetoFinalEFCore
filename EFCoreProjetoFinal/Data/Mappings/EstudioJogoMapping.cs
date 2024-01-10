using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class EstudioJogoMapping : IEntityTypeConfiguration<EstudioJogo>
    {
        public void Configure(EntityTypeBuilder<EstudioJogo> builder)
        {
            builder.ToTable("EstudioJogo");

            builder.HasKey(p => new { p.JogosId, p.EstudioId });

            builder.Property(p => p.JogosId)
                .IsRequired()
                .HasColumnName("JogosId");

            builder.Property(p => p.EstudioId)
                .IsRequired()
                .HasColumnName("EstudioId");

            builder.HasOne(p => p.Jogo)
                .WithMany(p => p.EstudioJogo)
                .HasForeignKey(p => p.JogosId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Estudio)
                .WithMany(c => c.EstudioJogo)
                .HasForeignKey(c => c.EstudioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
