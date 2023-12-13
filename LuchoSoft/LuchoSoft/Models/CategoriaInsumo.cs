using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class CategoriaInsumo
    {
        public CategoriaInsumo()
        {
            Insumos = new HashSet<Insumo>();
        }

        public int IdCategoriaInsumos { get; set; }
        public string NombreCategoriaInsumos { get; set; } = null!;
        public byte EstadoCategoriaInsumos { get; set; }

        public virtual ICollection<Insumo> Insumos { get; set; }
    }
}
