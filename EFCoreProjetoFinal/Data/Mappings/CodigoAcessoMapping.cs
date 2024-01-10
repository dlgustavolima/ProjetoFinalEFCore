using EFCoreProjetoFinal.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreProjetoFinal.Data.Mappings
{
    public class CodigoAcessoMapping : IEntityTypeConfiguration<CodigoAcesso>
    {
        public void Configure(EntityTypeBuilder<CodigoAcesso> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .IsRequired()
                .HasColumnType("varchar(25)");

            builder.Property(p => p.Ativo);

            builder.Property(p => p.DataAtivacao);

            builder.Property(p => p.DataExpiracao);

            builder.Property(p => p.DataExpiracao);

            builder.HasMany(p => p.Jogos)
                .WithOne()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
