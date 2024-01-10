using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class EstudiosMapping : IEntityTypeConfiguration<Estudio>
    {
        public void Configure(EntityTypeBuilder<Estudio> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Empresa)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(p => p.EstudioJogo)
                .WithOne(p => p.Estudio)
                .HasForeignKey(p => p.EstudioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Estudios");
        }
    }
}
