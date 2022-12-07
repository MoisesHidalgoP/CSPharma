using System;
using System.Collections.Generic;

namespace DAL.Modelo
{
    public partial class DlkCatAccEmpleado
    {
        public string? MdUuid { get; set; }
        public string CodEmpleado { get; set; } = null!;
        public string ClaveEmpleado { get; set; } = null!;
        public string? MdDate { get; set; }
        public string? NivelAccesoEmpleado { get; set; }
    }
}
