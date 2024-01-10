using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class GeneroMapping : IEntityTypeConfiguration<Genero>
    {
        public void Configure(EntityTypeBuilder<Genero> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(p => p.GeneroJogo)
                .WithOne(p => p.Genero)
                .HasForeignKey(p => p.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Generos");
        }
    }
}
