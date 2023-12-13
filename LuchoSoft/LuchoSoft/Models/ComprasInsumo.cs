using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class ComprasInsumo
    {
        public int IdComprasInsumos { get; set; }
        public int CantidadInsumoComprasInsumos { get; set; }
        public double PrecioInsumoComprasInsumos { get; set; }
        public int? IdCompraComprasInsumos { get; set; }
        public int? IdInsumoComprasInsumos { get; set; }

        public virtual Compra? IdCompraComprasInsumosNavigation { get; set; }
        public virtual Insumo? IdInsumoComprasInsumosNavigation { get; set; }
    }
}
