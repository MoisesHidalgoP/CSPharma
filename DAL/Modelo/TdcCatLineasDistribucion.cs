using System;
using System.Collections.Generic;

namespace DAL.Modelo
{
    public partial class TdcCatLineasDistribucion
    {
        public TdcCatLineasDistribucion()
        {
            TdcTchEstadoPedidos = new HashSet<TdcTchEstadoPedido>();
        }

        /// <summary>
        /// Código de de metadato que indica el grupo de inserción al que pertenece el registro.
        /// 
        /// 
        /// </summary>
        public string MdUuid { get; set; } = null!;
        /// <summary>
        /// Fecha en la que se define el grupo de inserción.
        /// 
        /// </summary>
        public DateTime MdDate { get; set; }
        /// <summary>
        /// Identificador unívoco de la línea de distribucíon en el sistema.
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Código que identifica de forma unívoca la línea de distribución por carretera que sigue el envío: codprovincia-codmunicipio-codbarrio.
        /// 
        /// </summary>
        public string CodLinea { get; set; } = null!;
        /// <summary>
        /// Código que identifica de forma unívoca a la provincia.
        /// 
        /// </summary>
        public string CodProvincia { get; set; } = null!;
        /// <summary>
        /// Código que identifica de forma unívoca el municipio.
        /// 
        /// 
        /// </summary>
        public string CodMunicipio { get; set; } = null!;
        /// <summary>
        /// Código que identifica de forma unívoca el barrio.
        /// 
        /// </summary>
        public string CodBarrio { get; set; } = null!;

        public virtual ICollection<TdcTchEstadoPedido> TdcTchEstadoPedidos { get; set; }
    }
}
