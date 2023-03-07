using System;
using System.Collections.Generic;

namespace DAL.Modelo;

public partial class TdcCatEstadosDevolucionPedido
{
    /// <summary>
    /// Código de metadato que indica el grupo de inserción al que pertenece el registro.
    /// 
    /// </summary>
    public string MdUuid { get; set; } = null!;

    /// <summary>
    /// Identificador unívoco del estado de devolución del pedido en el sistema.
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Código que identifica de forma unívoca el estado de devolución de un pedido.
    /// 
    /// </summary>
    public string CodEstadoDevolucion { get; set; } = null!;

    /// <summary>
    /// Descripción del estado de devolución del pedido.
    /// 
    /// </summary>
    public string? DesEstadoDevolucion { get; set; }

    public DateTime MdDate { get; set; }

    public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; } = new List<TdcTchEstadoPedido>();
}
