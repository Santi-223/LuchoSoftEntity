using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LuchoSoft.Models
{
    public partial class LuchoSoftV1Context : DbContext
    {
        public LuchoSoftV1Context()
        {
        }

        public LuchoSoftV1Context(DbContextOptions<LuchoSoftV1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<CategoriaInsumo> CategoriaInsumos { get; set; } = null!;
        public virtual DbSet<CategoriaProducto> CategoriaProductos { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<ComprasInsumo> ComprasInsumos { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Insumo> Insumos { get; set; } = null!;
        public virtual DbSet<OrdenInsumo> OrdenInsumos { get; set; } = null!;
        public virtual DbSet<OrdenesDeProduccion> OrdenesDeProduccions { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<PedidosProducto> PedidosProductos { get; set; } = null!;
        public virtual DbSet<Permiso> Permisos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Proveedore> Proveedores { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolesPermiso> RolesPermisos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
/*#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-8CAFCVH\\SQLEXPRESS;Initial Catalog=LuchoSoftV1;Integrated Security=True;");*/
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaInsumo>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaInsumos)
                    .HasName("PK__Categori__091B8E435D10647E");

                entity.ToTable("Categoria_insumos");

                entity.Property(e => e.IdCategoriaInsumos).HasColumnName("Id_categoria_insumos");

                entity.Property(e => e.EstadoCategoriaInsumos)
                    .HasColumnName("Estado_categoria_insumos")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreCategoriaInsumos)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_categoria_insumos");
            });

            modelBuilder.Entity<CategoriaProducto>(entity =>
            {
                entity.HasKey(e => e.IdCategoriaProductos)
                    .HasName("PK__Categori__E0096CE9B26812B6");

                entity.ToTable("Categoria_productos");

                entity.Property(e => e.IdCategoriaProductos).HasColumnName("Id_categoria_productos");

                entity.Property(e => e.EstadoCategoriaProductos)
                    .HasColumnName("Estado_categoria_productos")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreCategoriaProductos)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_categoria_productos");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__FCE03992952198C2");

                entity.Property(e => e.IdCliente)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_cliente");

                entity.Property(e => e.ClienteFrecuente).HasColumnName("Cliente_frecuente");

                entity.Property(e => e.DireccionCliente)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Direccion_cliente");

                entity.Property(e => e.EstadoCliente)
                    .HasColumnName("Estado_cliente")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_cliente");

                entity.Property(e => e.TelefonoCliente)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("Telefono_cliente");
            });

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasKey(e => e.IdCompra)
                    .HasName("PK__Compras__957E332F67B98B66");

                entity.Property(e => e.IdCompra).HasColumnName("Id_compra");

                entity.Property(e => e.EstadoCompra).HasColumnName("Estado_compra");

                entity.Property(e => e.FechaCompra)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_compra");

                entity.Property(e => e.IdProveedorCompras).HasColumnName("Id_proveedor_compras");

                entity.Property(e => e.NombreCompra)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_compra");

                entity.Property(e => e.TotalCompra).HasColumnName("Total_compra");

                entity.HasOne(d => d.IdProveedorComprasNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdProveedorCompras)
                    .HasConstraintName("FK__Compras__Id_prov__45F365D3");
            });

            modelBuilder.Entity<ComprasInsumo>(entity =>
            {
                entity.HasKey(e => e.IdComprasInsumos)
                    .HasName("PK__Compras___82E877333BCA6383");

                entity.ToTable("Compras_insumos");

                entity.Property(e => e.IdComprasInsumos).HasColumnName("Id_compras_insumos");

                entity.Property(e => e.CantidadInsumoComprasInsumos).HasColumnName("Cantidad_insumo_compras_insumos");

                entity.Property(e => e.IdCompraComprasInsumos).HasColumnName("Id_compra_compras_insumos");

                entity.Property(e => e.IdInsumoComprasInsumos).HasColumnName("Id_insumo_compras_insumos");

                entity.Property(e => e.PrecioInsumoComprasInsumos).HasColumnName("Precio_insumo_compras_insumos");

                entity.HasOne(d => d.IdCompraComprasInsumosNavigation)
                    .WithMany(p => p.ComprasInsumos)
                    .HasForeignKey(d => d.IdCompraComprasInsumos)
                    .HasConstraintName("FK__Compras_i__Id_co__4CA06362");

                entity.HasOne(d => d.IdInsumoComprasInsumosNavigation)
                    .WithMany(p => p.ComprasInsumos)
                    .HasForeignKey(d => d.IdInsumoComprasInsumos)
                    .HasConstraintName("FK__Compras_i__Id_in__4D94879B");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__01AC2829273A6A6B");

                entity.Property(e => e.IdEmpleado).HasColumnName("Id_empleado");

                entity.Property(e => e.DireccionEmpleado)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Direccion_empleado");

                entity.Property(e => e.EstadoEmpleado)
                    .HasColumnName("Estado_empleado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ImagenEmpleado).HasColumnName("Imagen_empleado");

                entity.Property(e => e.NombreEmpleado)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_empleado");

                entity.Property(e => e.TelefonoEmpleado)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("Telefono_empleado");
            });

            modelBuilder.Entity<Insumo>(entity =>
            {
                entity.HasKey(e => e.IdInsumo)
                    .HasName("PK__Insumos__2DF06549DCB54350");

                entity.Property(e => e.IdInsumo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_insumo");

                entity.Property(e => e.EstadoInsumo)
                    .HasColumnName("Estado_insumo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdCategoriaInsumoInsumos).HasColumnName("Id_categoria_insumo_insumos");

                entity.Property(e => e.ImagenInsumo).HasColumnName("Imagen_insumo");

                entity.Property(e => e.NombreInsumo)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_insumo");

                entity.Property(e => e.StockInsumo).HasColumnName("Stock_insumo");

                entity.Property(e => e.UnidadesDeMedidaInsumo)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("UnidadesDeMedida_insumo");

                entity.HasOne(d => d.IdCategoriaInsumoInsumosNavigation)
                    .WithMany(p => p.Insumos)
                    .HasForeignKey(d => d.IdCategoriaInsumoInsumos)
                    .HasConstraintName("FK__Insumos__Id_cate__49C3F6B7");
            });

            modelBuilder.Entity<OrdenInsumo>(entity =>
            {
                entity.HasKey(e => e.IdOrdenInsumos)
                    .HasName("PK__Orden_in__BDDC650EADE4CB6B");

                entity.ToTable("Orden_insumos");

                entity.Property(e => e.IdOrdenInsumos).HasColumnName("Id_orden_insumos");

                entity.Property(e => e.CantidadInsumoOrdenInsumos).HasColumnName("Cantidad_insumo_orden_insumos");

                entity.Property(e => e.DescripcionOrdenInsumos)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion_orden_insumos");

                entity.Property(e => e.IdInsumoOrdenInsumos).HasColumnName("Id_insumo_orden_insumos");

                entity.Property(e => e.IdOrdenDeProduccionOrdenInsumos).HasColumnName("Id_orden_de_produccion_orden_insumos");

                entity.HasOne(d => d.IdInsumoOrdenInsumosNavigation)
                    .WithMany(p => p.OrdenInsumos)
                    .HasForeignKey(d => d.IdInsumoOrdenInsumos)
                    .HasConstraintName("FK__Orden_ins__Id_in__628FA481");

                entity.HasOne(d => d.IdOrdenDeProduccionOrdenInsumosNavigation)
                    .WithMany(p => p.OrdenInsumos)
                    .HasForeignKey(d => d.IdOrdenDeProduccionOrdenInsumos)
                    .HasConstraintName("FK__Orden_ins__Id_or__619B8048");
            });

            modelBuilder.Entity<OrdenesDeProduccion>(entity =>
            {
                entity.HasKey(e => e.IdOrdenDeProduccion)
                    .HasName("PK__Ordenes___F3C58E85792A708C");

                entity.ToTable("Ordenes_de_produccion");

                entity.Property(e => e.IdOrdenDeProduccion).HasColumnName("Id_orden_de_produccion");

                entity.Property(e => e.DescripcionOrden)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion_orden");

                entity.Property(e => e.FechaOrden)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_orden");

                entity.Property(e => e.IdEmpleadoOrdenesDeProduccion).HasColumnName("Id_empleado_ordenes_de_produccion");

                entity.HasOne(d => d.IdEmpleadoOrdenesDeProduccionNavigation)
                    .WithMany(p => p.OrdenesDeProduccions)
                    .HasForeignKey(d => d.IdEmpleadoOrdenesDeProduccion)
                    .HasConstraintName("FK__Ordenes_d__Id_em__5EBF139D");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido)
                    .HasName("PK__Pedidos__1E657E9E8FA97CD4");

                entity.Property(e => e.IdPedido).HasColumnName("Id_pedido");

                entity.Property(e => e.EstadoPedido).HasColumnName("Estado_pedido");

                entity.Property(e => e.FechaPedido)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_pedido");

                entity.Property(e => e.FechaVenta)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_venta");

                entity.Property(e => e.IdClientePedidos).HasColumnName("Id_cliente_pedidos");

                entity.Property(e => e.IdEmpleadoPedidos).HasColumnName("Id_empleado_pedidos");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPedido).HasColumnName("Total_pedido");

                entity.Property(e => e.TotalVenta).HasColumnName("Total_venta");

                entity.HasOne(d => d.IdClientePedidosNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdClientePedidos)
                    .HasConstraintName("FK__Pedidos__Id_clie__534D60F1");

                entity.HasOne(d => d.IdEmpleadoPedidosNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEmpleadoPedidos)
                    .HasConstraintName("FK__Pedidos__Id_empl__5441852A");
            });

            modelBuilder.Entity<PedidosProducto>(entity =>
            {
                entity.HasKey(e => e.IdPedidosProductos)
                    .HasName("PK__Pedidos___CF5231550627EE3C");

                entity.ToTable("Pedidos_productos");

                entity.Property(e => e.IdPedidosProductos).HasColumnName("Id_pedidos_productos");

                entity.Property(e => e.CantidadProducto).HasColumnName("Cantidad_producto");

                entity.Property(e => e.FechaPedidoProducto)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_pedido_producto");

                entity.Property(e => e.IdPedidoPedidosProductos).HasColumnName("Id_pedido_pedidos_productos");

                entity.Property(e => e.IdProductoPedidosProductos).HasColumnName("Id_producto_pedidos_productos");

                entity.HasOne(d => d.IdPedidoPedidosProductosNavigation)
                    .WithMany(p => p.PedidosProductos)
                    .HasForeignKey(d => d.IdPedidoPedidosProductos)
                    .HasConstraintName("FK__Pedidos_p__Id_pe__5AEE82B9");

                entity.HasOne(d => d.IdProductoPedidosProductosNavigation)
                    .WithMany(p => p.PedidosProductos)
                    .HasForeignKey(d => d.IdProductoPedidosProductos)
                    .HasConstraintName("FK__Pedidos_p__Id_pr__5BE2A6F2");
            });

            modelBuilder.Entity<Permiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso)
                    .HasName("PK__Permisos__A5D405E8FCE3A95A");

                entity.Property(e => e.IdPermiso).HasColumnName("Id_permiso");

                entity.Property(e => e.EstadoPermiso)
                    .HasColumnName("Estado_permiso")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombrePermiso)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_permiso");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto)
                    .HasName("PK__Producto__1D8EFF01AF27200F");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_producto");

                entity.Property(e => e.DescripcionProducto)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion_producto");

                entity.Property(e => e.EstadoProducto)
                    .HasColumnName("Estado_producto")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdCategoriaProductoProductos).HasColumnName("Id_categoria_producto_productos");

                entity.Property(e => e.ImagenProducto).HasColumnName("Imagen_producto");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_producto");

                entity.Property(e => e.PrecioProducto).HasColumnName("Precio_producto");

                entity.HasOne(d => d.IdCategoriaProductoProductosNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCategoriaProductoProductos)
                    .HasConstraintName("FK__Productos__Id_ca__5812160E");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.IdProveedor)
                    .HasName("PK__Proveedo__6704E5A89E5BBB7C");

                entity.Property(e => e.IdProveedor)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_proveedor");

                entity.Property(e => e.DireccionProveedor)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Direccion_proveedor");

                entity.Property(e => e.EstadoProveedor)
                    .HasColumnName("Estado_proveedor")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreProveedor)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_proveedor");

                entity.Property(e => e.TelefonoProveedor)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("Telefono_proveedor");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Roles__2D95A8949A27F324");

                entity.Property(e => e.IdRol).HasColumnName("Id_rol");

                entity.Property(e => e.DescripcionRol)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("Descripcion_rol");

                entity.Property(e => e.EstadoRol)
                    .HasColumnName("Estado_rol")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NombreRol)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_rol");
            });

            modelBuilder.Entity<RolesPermiso>(entity =>
            {
                entity.HasKey(e => e.IdRolesPermisos)
                    .HasName("PK__Roles_pe__CB14C42EE10240DA");

                entity.ToTable("Roles_permisos");

                entity.Property(e => e.IdRolesPermisos).HasColumnName("Id_roles_permisos");

                entity.Property(e => e.FechaRolesPermisos)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_roles_permisos");

                entity.Property(e => e.IdPermisoRolesPermisos).HasColumnName("Id_permiso_roles_permisos");

                entity.Property(e => e.IdRolRolesPermisos).HasColumnName("Id_rol_roles_permisos");

                entity.HasOne(d => d.IdPermisoRolesPermisosNavigation)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.IdPermisoRolesPermisos)
                    .HasConstraintName("FK__Roles_per__Id_pe__6D0D32F4");

                entity.HasOne(d => d.IdRolRolesPermisosNavigation)
                    .WithMany(p => p.RolesPermisos)
                    .HasForeignKey(d => d.IdRolRolesPermisos)
                    .HasConstraintName("FK__Roles_per__Id_ro__6C190EBB");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuarios__EF59F76272E05A29");

                entity.Property(e => e.IdUsuario)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_usuario");

                entity.Property(e => e.Contraseña)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.EstadoUsuario)
                    .HasColumnName("Estado_usuario")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdRolUsuarios).HasColumnName("Id_rol_usuarios");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_usuario");

                entity.HasOne(d => d.IdRolUsuariosNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdRolUsuarios)
                    .HasConstraintName("FK__Usuarios__Id_rol__66603565");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
