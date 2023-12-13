using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string TelefonoCliente { get; set; } = null!;
        public string DireccionCliente { get; set; } = null!;
        public byte ClienteFrecuente { get; set; }
        public byte EstadoCliente { get; set; }

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
