using System;
using System.Collections.Generic;

namespace DAL.Modelo;

public partial class DlkCatRole
{
    public string Descripcion { get; set; } = null!;

    public long NivelAccesoEmpleado { get; set; }

    public virtual ICollection<DlkCatAccEmpleado> DlkCatAccEmpleados { get; } = new List<DlkCatAccEmpleado>();
}
