using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Insumo
    {
        public Insumo()
        {
            ComprasInsumos = new HashSet<ComprasInsumo>();
            OrdenInsumos = new HashSet<OrdenInsumo>();
        }

        public int IdInsumo { get; set; }
        public byte[]? ImagenInsumo { get; set; }
        public string NombreInsumo { get; set; } = null!;
        public string UnidadesDeMedidaInsumo { get; set; } = null!;
        public double StockInsumo { get; set; }
        public byte EstadoInsumo { get; set; }
        public int? IdCategoriaInsumoInsumos { get; set; }

        public virtual CategoriaInsumo? IdCategoriaInsumoInsumosNavigation { get; set; }
        public virtual ICollection<ComprasInsumo> ComprasInsumos { get; set; }
        public virtual ICollection<OrdenInsumo> OrdenInsumos { get; set; }
    }
}
