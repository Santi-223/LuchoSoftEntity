using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public byte EstadoUsuario { get; set; }
        public int? IdRolUsuarios { get; set; }

        public virtual Role? IdRolUsuariosNavigation { get; set; }
    }
}
