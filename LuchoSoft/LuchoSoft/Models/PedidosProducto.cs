using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class PedidosProducto
    {
        public int IdPedidosProductos { get; set; }
        public DateTime FechaPedidoProducto { get; set; }
        public double CantidadProducto { get; set; }
        public double Subtotal { get; set; }
        public int? IdProductoPedidosProductos { get; set; }
        public int? IdPedidoPedidosProductos { get; set; }

        public virtual Pedido? IdPedidoPedidosProductosNavigation { get; set; }
        public virtual Producto? IdProductoPedidosProductosNavigation { get; set; }
    }
}
