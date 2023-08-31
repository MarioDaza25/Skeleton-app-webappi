using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class CiudadConfiguration : IEntityTypeConfiguration<Ciudad>
{
    public void Configure(EntityTypeBuilder<Ciudad> builder)
    {
        builder.ToTable("ciudad");

        builder.Property(p => p.NombreCiudad)
        .IsRequired()
        .HasMaxLength(50);

        builder.HasOne(p => p.Departamento)
        .WithMany(p => p.Ciudades)
        .HasForeignKey(p => p.IdDepartamentoFk);
    }
}
