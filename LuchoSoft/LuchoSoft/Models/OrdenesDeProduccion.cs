using System;
using System.Collections.Generic;

namespace LuchoSoft.Models
{
    public partial class OrdenesDeProduccion
    {
        public OrdenesDeProduccion()
        {
            OrdenInsumos = new HashSet<OrdenInsumo>();
        }

        public int IdOrdenDeProduccion { get; set; }
        public string DescripcionOrden { get; set; } = null!;
        public DateTime FechaOrden { get; set; }
        public int? IdEmpleadoOrdenesDeProduccion { get; set; }

        public virtual Empleado? IdEmpleadoOrdenesDeProduccionNavigation { get; set; }
        public virtual ICollection<OrdenInsumo> OrdenInsumos { get; set; }
    }
}
