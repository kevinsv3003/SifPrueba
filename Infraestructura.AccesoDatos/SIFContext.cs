
using Dominio.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infraestructura.AccesoDatos
{
    public class SIFContext : IdentityDbContext<UsuarioApp, RolApp, Guid, UsuarioNotificacion, UsuarioRol, UsuarioLogin, RolNotificacion, UsuarioToken>
    {
        public SIFContext()
        {
        }

        public SIFContext(DbContextOptions<SIFContext> options): base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(@"Data Source=KSV-PC;Initial Catalog=sifdesar;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UsuarioApp>().ToTable("Usuarios", "SEG");
            builder.Entity<RolApp>().ToTable("Roles", "SEG");
            builder.Entity<UsuarioNotificacion>().ToTable("UsuariosNotificaciones", "SEG");
            builder.Entity<UsuarioRol>().ToTable("UsuariosRoles", "SEG");
            builder.Entity<UsuarioLogin>().ToTable("UsuariosLogin", "SEG");
            builder.Entity<RolNotificacion>().ToTable("RolesNotificaciones", "SEG");
            builder.Entity<UsuarioToken>().ToTable("UsuariosToken", "SEG");
            builder.Entity<Renta>().ToTable("Rentas", "SEG");

            builder.Entity<Menu>().ToTable("Menu", "MENU");
            builder.Entity<RolesMenu>().ToTable("RolesMenu", "MENU");

            builder.Entity<LogTransaccion>().ToTable("LogTransaccion", "AUDI");
            builder.Entity<LogError>().ToTable("LogError", "AUDI");
        }

        //DETALLAR LOS DBSET QUE SE UTILIZARAN
        public DbSet<Menu> Menus { get; set; }
        public DbSet<RolesMenu> RolesMenus { get; set; }
        public DbSet<LogError> LogErrors { get; set; }
        public DbSet<LogTransaccion> LogTransaccions { get; set; }
        public DbSet<Renta> Rentas { get; set; }




    }
}
