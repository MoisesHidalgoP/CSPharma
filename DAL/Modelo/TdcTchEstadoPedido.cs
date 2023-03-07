using System;
using System.Collections.Generic;

namespace DAL.Modelo;

public partial class TdcTchEstadoPedido
{
    /// <summary>
    /// Código de metadato que indica el grupo de inserción al que pertenece el registro.
    /// </summary>
    public string MdUuid { get; set; } = null!;

    /// <summary>
    /// Fecha en la que se define el grupo de inserción.
    /// 
    /// </summary>
    public DateTime MdDate { get; set; }

    /// <summary>
    /// Identificador unívoco del pedido en el sistema.
    /// 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Código que identifica de forma unívoca el estado de envío de un pedido.
    /// 
    /// </summary>
    public string? CodEstadoEnvio { get; set; }

    /// <summary>
    /// Código que identifica de forma unívoca el estado de pago de un pedido.
    /// 
    /// </summary>
    public string? CodEstadoPago { get; set; }

    /// <summary>
    /// Código que identifica de forma unívoca el estado de devolución de un pedido.
    /// 
    /// </summary>
    public string? CodEstadoDevolucion { get; set; }

    /// <summary>
    /// Código que identifica de forma unívoca un pedido. Se construye con: provincia-codfarmacia-id.
    /// 
    /// </summary>
    public string CodPedido { get; set; } = null!;

    /// <summary>
    /// Código que identifica de forma unívoca la línea de distribución por carretera que sigue el envío: codprovincia-codmunicipio-codbarrio.
    /// 
    /// </summary>
    public string CodLinea { get; set; } = null!;

    public virtual TdcCatEstadosDevolucionPedido? CodEstadoDevolucionNavigation { get; set; }

    public virtual TdcCatEstadosEnvioPedido? CodEstadoEnvioNavigation { get; set; }

    public virtual TdcCatEstadosPagoPedido? CodEstadoPagoNavigation { get; set; }

    public virtual TdcCatLineasDistribucion CodLineaNavigation { get; set; } = null!;
}
