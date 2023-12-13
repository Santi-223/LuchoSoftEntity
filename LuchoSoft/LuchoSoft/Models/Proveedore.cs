using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            Compras = new HashSet<Compra>();
        }

        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; } = null!;
        public string TelefonoProveedor { get; set; } = null!;
        public string DireccionProveedor { get; set; } = null!;
        public byte EstadoProveedor { get; set; }

        public virtual ICollection<Compra> Compras { get; set; }
    }
}
