using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            RolesPermisos = new HashSet<RolesPermiso>();
        }

        public int IdPermiso { get; set; }
        public string NombrePermiso { get; set; } = null!;
        public byte EstadoPermiso { get; set; }

        public virtual ICollection<RolesPermiso> RolesPermisos { get; set; }
    }
}
