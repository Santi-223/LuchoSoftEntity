using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Compra
    {
        public Compra()
        {
            ComprasInsumos = new HashSet<ComprasInsumo>();
        }

        public int IdCompra { get; set; }
        public string NombreCompra { get; set; } = null!;
        public DateTime FechaCompra { get; set; }
        public int EstadoCompra { get; set; }
        public double TotalCompra { get; set; }
        public int? IdProveedorCompras { get; set; }

        public virtual Proveedore? IdProveedorComprasNavigation { get; set; }
        public virtual ICollection<ComprasInsumo> ComprasInsumos { get; set; }
    }
}
