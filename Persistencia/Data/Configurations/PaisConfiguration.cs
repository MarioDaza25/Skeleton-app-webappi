using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia.Data.Configurations;

public class PaisConfiguration : IEntityTypeConfiguration<Pais>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Pais> builder)
    {
        builder.ToTable("pais");

        builder.Property(p => p.NombrePais)
        .IsRequired()
        .HasMaxLength(50);
    }

    
}
