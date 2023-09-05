using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");

            builder.Property(p => p.Username)
            .IsRequired()
            .HasMaxLength(200);

            builder.HasIndex(p => new {
                p.Username,
                p.Email
            }).HasDatabaseName("IX_MiIndice")
            .IsUnique();

            builder.Property(p => p.Email)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.Password)
            .IsRequired()
            .HasMaxLength(50);

            builder.HasMany(p => p.Roles)
            .WithMany(p => p.Usuarios)
            .UsingEntity<UsuarioRol>(
                j => j
                    .HasOne(pt => pt.Rol)
                    .WithMany(t => t.UsuarioRoles)
                    .HasForeignKey(pt => pt.IdRolFk),

                j => j
                    .HasOne(pt => pt.Usuario)
                    .WithMany(t => t.UsuarioRoles)
                    .HasForeignKey(pt => pt.IdUsuarioFk),

                j => 
                {
                    j.HasKey(t => new {t.IdRolFk, t.IdUsuarioFk});
                });
            }
        
        
        
    }
}