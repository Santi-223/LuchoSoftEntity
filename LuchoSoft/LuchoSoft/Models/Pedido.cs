using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            PedidosProductos = new HashSet<PedidosProducto>();
        }

        public int IdPedido { get; set; }
        public string? Observaciones { get; set; }
        public DateTime FechaVenta { get; set; }
        public DateTime FechaPedido { get; set; }
        public int EstadoPedido { get; set; }
        public double TotalVenta { get; set; }
        public double TotalPedido { get; set; }
        public int? IdClientePedidos { get; set; }
        public int? IdEmpleadoPedidos { get; set; }

        public virtual Cliente? IdClientePedidosNavigation { get; set; }
        public virtual Empleado? IdEmpleadoPedidosNavigation { get; set; }
        public virtual ICollection<PedidosProducto> PedidosProductos { get; set; }
    }
}
