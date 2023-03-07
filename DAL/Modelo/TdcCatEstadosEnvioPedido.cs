using System;
using System.Collections.Generic;

namespace DAL.Modelo;

public partial class TdcCatEstadosEnvioPedido
{
    /// <summary>
    /// Código de metadato que indica el grupo de inserción al que pertenece el registro.
    /// 
    /// </summary>
    public string MdUuid { get; set; } = null!;

    /// <summary>
    /// Fecha en la que se define le grupo de inserción.
    /// 
    /// </summary>
    public DateTime MdDate { get; set; }

    /// <summary>
    /// Identificador unívoco del estado de envío del pedido
    /// en el sistema.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Código que identifica de forma unívoca el estado de envío de un pedido.
    /// 
    /// </summary>
    public string CodEstadoEnvio { get; set; } = null!;

    /// <summary>
    /// Descripción del estado de envío del pedido.
    /// 
    /// </summary>
    public string? DesEstadoEnvio { get; set; }

    public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; } = new List<TdcTchEstadoPedido>();
}
