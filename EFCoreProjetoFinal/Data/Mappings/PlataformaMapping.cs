using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class PlataformaMapping : IEntityTypeConfiguration<Plataforma>
    {
        public void Configure(EntityTypeBuilder<Plataforma> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .HasColumnType("varchar(100)");

            builder.HasMany(p => p.JogoPlataforma)
                .WithOne(p => p.Plataforma)
                .HasForeignKey(p => p.PlataformaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Plataformas");
        }
    }
}
