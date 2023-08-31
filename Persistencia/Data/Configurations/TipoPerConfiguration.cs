using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class TipoPerConfiguration : IEntityTypeConfiguration<TipoPersona>
    {
        public void Configure(EntityTypeBuilder<TipoPersona> builder)
        {
            builder.ToTable("tipoPersona");

            builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(50);
        }
    }
}