using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations;

public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.Property(p => p.IdPersona)
        .IsRequired()
        .HasMaxLength(15);

        builder.Property(p => p.Nombre)
        .IsRequired()
        .HasMaxLength(25);

        builder.Property(p => p.Apellido)
        .IsRequired()
        .HasMaxLength(25);

        builder.HasOne(p => p.Genero)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdGeneroFk);

        builder.HasOne(p => p.Ciudad)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdCiudadFk);

        builder.HasOne(p => p.TipoPersona)
        .WithMany(p => p.Personas)
        .HasForeignKey(p => p.IdTPerFk);

        builder.HasMany(p => p.Salones)
        .WithMany(p => p.Personas)
        .UsingEntity<TrainerSalon>(
            j => j
                .HasOne(pt => pt.Salon)
                .WithMany(t => t.TrainerSalones)
                .HasForeignKey(pt => pt.IdSalonFk),

            j => j
                .HasOne(pt => pt.Persona)
                .WithMany(t => t.TrainerSalones)
                .HasForeignKey(pt => pt.IdPersonaFk),

            j => 
            {
                j.HasKey(t => new {t.IdSalonFk, t.IdPersonaFk});
            }
        );
    }
}
