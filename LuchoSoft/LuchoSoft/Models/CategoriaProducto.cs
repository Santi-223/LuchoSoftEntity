using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class CategoriaProducto
    {
        public CategoriaProducto()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoriaProductos { get; set; }
        public string NombreCategoriaProductos { get; set; } = null!;
        public byte EstadoCategoriaProductos { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
