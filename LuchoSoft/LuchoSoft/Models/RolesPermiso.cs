using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class RolesPermiso
    {
        public int IdRolesPermisos { get; set; }
        public DateTime FechaRolesPermisos { get; set; }
        public int? IdRolRolesPermisos { get; set; }
        public int? IdPermisoRolesPermisos { get; set; }

        public virtual Permiso? IdPermisoRolesPermisosNavigation { get; set; }
        public virtual Role? IdRolRolesPermisosNavigation { get; set; }
    }
}
