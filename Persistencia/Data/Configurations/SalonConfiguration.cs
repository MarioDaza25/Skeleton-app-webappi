using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class SalonConfiguration : IEntityTypeConfiguration<Salon>
    {
        public void Configure(EntityTypeBuilder<Salon> builder)
        {
            builder.ToTable("Salon");

            builder.Property(p => p.Nombresalon)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.Capacidad)
            .HasColumnType("int");
        }
    }
}