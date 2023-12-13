using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class OrdenInsumo
    {
        public int IdOrdenInsumos { get; set; }
        public string? DescripcionOrdenInsumos { get; set; }
        public double CantidadInsumoOrdenInsumos { get; set; }
        public int? IdOrdenDeProduccionOrdenInsumos { get; set; }
        public int? IdInsumoOrdenInsumos { get; set; }

        public virtual Insumo? IdInsumoOrdenInsumosNavigation { get; set; }
        public virtual OrdenesDeProduccion? IdOrdenDeProduccionOrdenInsumosNavigation { get; set; }
    }
}
