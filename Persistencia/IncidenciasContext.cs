﻿using System.Reflection;
using Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistencia;

public class IncidenciasContext : DbContext
{
    public IncidenciasContext(DbContextOptions<IncidenciasContext> options) : base(options){}

    public DbSet<Pais> Paises { get; set; }
    public DbSet<Departamento> Departamentos { get; set; }
    public DbSet<Ciudad> Ciudades { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<TipoPersona> TipoPersonas { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Salon> Salones { get; set; }
    public DbSet<TrainerSalon> TrainerSalones { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<UsuarioRol> UsuarioRoles { get; set; }
    public DbSet<Rol> Roles { get; set; }

    
    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
