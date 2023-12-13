using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Producto
    {
        public Producto()
        {
            PedidosProductos = new HashSet<PedidosProducto>();
        }

        public int IdProducto { get; set; }
        public byte[]? ImagenProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public string DescripcionProducto { get; set; } = null!;
        public byte EstadoProducto { get; set; }
        public double PrecioProducto { get; set; }
        public int? IdCategoriaProductoProductos { get; set; }

        public virtual CategoriaProducto? IdCategoriaProductoProductosNavigation { get; set; }
        public virtual ICollection<PedidosProducto> PedidosProductos { get; set; }
    }
}
