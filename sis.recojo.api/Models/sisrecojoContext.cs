using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace sis.recojo.api.Models
{
    public partial class sisrecojoContext : DbContext
    {
        public sisrecojoContext()
        {
        }

        public sisrecojoContext(DbContextOptions<sisrecojoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buzon> Buzons { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Registro> Registros { get; set; } = null!;
        public virtual DbSet<Solicitude> Solicitudes { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PPLNINA;Initial Catalog=sisrecojo;User id=christoper;pwd=DYF@CHRISTOPER");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buzon>(entity =>
            {
                entity.ToTable("buzon");

                entity.Property(e => e.BuzonId).HasColumnName("buzon_id");

                entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Solicitud)
                    .WithMany(p => p.Buzons)
                    .HasForeignKey(d => d.SolicitudId)
                    .HasConstraintName("FK_solicitud_id_buzon");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Buzons)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_usuario_id_buzon");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Ruc)
                    .HasName("PK__clientes__C2B74E6033AD60E4");

                entity.ToTable("clientes");

                entity.Property(e => e.Ruc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ruc")
                    .IsFixedLength();

                entity.Property(e => e.Nombre)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.Ubicacion)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("ubicacion");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_usuario_id_clientes");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estados");

                entity.Property(e => e.EstadoId).HasColumnName("estado_id");

                entity.Property(e => e.Estado1)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<Registro>(entity =>
            {
                entity.ToTable("registros");

                entity.Property(e => e.RegistroId)
                    .ValueGeneratedNever()
                    .HasColumnName("registro_id");

                entity.Property(e => e.EstadoId).HasColumnName("estado_id");

                entity.Property(e => e.Ruc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ruc")
                    .IsFixedLength();

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.HasOne(d => d.Estado)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.EstadoId)
                    .HasConstraintName("FK_estado_id_registros");

                entity.HasOne(d => d.RucNavigation)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.Ruc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ruc_registros");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Registros)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_usuario_id_registros");
            });

            modelBuilder.Entity<Solicitude>(entity =>
            {
                entity.HasKey(e => e.SolicitudId)
                    .HasName("PK__solicitu__0CB3B442E0A69510");

                entity.ToTable("solicitudes");

                entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");

                entity.Property(e => e.Solicitud)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("solicitud");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios");

                entity.HasIndex(e => e.usuario, "UQ__usuarios__9AFF8FC6E902D070")
                    .IsUnique();

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.Property(e => e.AdministradorId).HasColumnName("administrador_id");

                entity.Property(e => e.AreaAsignada)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("area_asignada");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Puesto)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("puesto");

                entity.Property(e => e.usuario)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.Administrador)
                    .WithMany(p => p.InverseAdministrador)
                    .HasForeignKey(d => d.AdministradorId)
                    .HasConstraintName("FK_administrador_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
