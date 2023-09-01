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

        builder.Property(p => p.Username)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.Email)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.Password)
        .IsRequired()
        .HasMaxLength(50);

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


        builder.HasMany(p => p.Roles)
        .WithMany(p => p.Personas)
        .UsingEntity<PersonaRol>(
            j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.PersonaRoles)
                .HasForeignKey(pt => pt.IdRolFk),

            j => j
                .HasOne(pt => pt.Persona)
                .WithMany(t => t.PersonaRoles)
                .HasForeignKey(pt => pt.IdUsuarioFk),

            j => 
            {
                j.HasKey(t => new {t.IdRolFk, t.IdUsuarioFk});
            }
        );
    }
}
