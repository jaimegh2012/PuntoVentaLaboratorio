using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POSApi.DB.PuntoVentaEntities;

public partial class PuntoVentaEntities : DbContext
{
    public PuntoVentaEntities()
    {
    }

    public PuntoVentaEntities(DbContextOptions<PuntoVentaEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<DireccionesCliente> DireccionesClientes { get; set; }

    public virtual DbSet<HistorialRefreshToken> HistorialRefreshTokens { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-LB1UBLV;Initial Catalog=PuntoVenta;user id=sa;password=1234;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.CodCategoria).HasName("PK__Categori__3D488B9007F7A548");

            entity.Property(e => e.NombreCategoria)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.CodCliente).HasName("PK__Clientes__DF8324D755759159");

            entity.Property(e => e.NombreCliente)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => e.CodDetallePedido).HasName("PK__DetalleP__85E226A5A5D21E28");

            entity.Property(e => e.Cantidad).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.CodPedidoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodPedido)
                .HasConstraintName("FK__DetallePe__CodPe__6D0D32F4");

            entity.HasOne(d => d.CodProductoNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.CodProducto)
                .HasConstraintName("FK__DetallePe__CodPr__6C190EBB");
        });

        modelBuilder.Entity<DireccionesCliente>(entity =>
        {
            entity.HasKey(e => e.CodDireccion).HasName("PK__Direccio__77C53DFD2C069242");

            entity.Property(e => e.Direccion)
                .HasMaxLength(300)
                .IsUnicode(false);

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.DireccionesClientes)
                .HasForeignKey(d => d.CodCliente)
                .HasConstraintName("FK__Direccion__CodCl__5070F446");
        });

        modelBuilder.Entity<HistorialRefreshToken>(entity =>
        {
            entity.HasKey(e => e.CodHistorialToken).HasName("PK__Historia__601E7634C528C92F");

            entity.ToTable("HistorialRefreshToken");

            entity.Property(e => e.EsActivo).HasComputedColumnSql("(case when [FechaExpiracion]<getutcdate() then CONVERT([bit],(0)) else CONVERT([bit],(1)) end)", false);
            entity.Property(e => e.FechaCreacion).HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracion).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.Token).IsUnicode(false);

            entity.HasOne(d => d.CodUsuarioNavigation).WithMany(p => p.HistorialRefreshTokens)
                .HasForeignKey(d => d.CodUsuario)
                .HasConstraintName("FK__Historial__CodUs__5FB337D6");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.CodPedido).HasName("PK__Pedidos__D1AFD9970D01C0E2");

            entity.Property(e => e.Total).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.CodClienteNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodCliente)
                .HasConstraintName("FK__Pedidos__CodClie__6754599E");

            entity.HasOne(d => d.CodDireccionNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodDireccion)
                .HasConstraintName("FK__Pedidos__CodDire__68487DD7");

            entity.HasOne(d => d.CodUsuarioNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.CodUsuario)
                .HasConstraintName("FK__Pedidos__CodUsua__693CA210");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.CodProducto).HasName("PK__Producto__0D06FDF35B98B9C5");

            entity.Property(e => e.NombreProducto)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(6, 2)");

            entity.HasOne(d => d.CodCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CodCategoria)
                .HasConstraintName("FK__Productos__CodCa__5535A963");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.CodUsuario).HasName("PK__Usuarios__FC30C2C4B41CE702");

            entity.Property(e => e.Clave).IsUnicode(false);
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Usuario1)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
