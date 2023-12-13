using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            OrdenesDeProduccions = new HashSet<OrdenesDeProduccion>();
            Pedidos = new HashSet<Pedido>();
        }

        public int IdEmpleado { get; set; }
        public byte[]? ImagenEmpleado { get; set; }
        public string NombreEmpleado { get; set; } = null!;
        public string TelefonoEmpleado { get; set; } = null!;
        public string DireccionEmpleado { get; set; } = null!;
        public byte EstadoEmpleado { get; set; }

        public virtual ICollection<OrdenesDeProduccion> OrdenesDeProduccions { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
