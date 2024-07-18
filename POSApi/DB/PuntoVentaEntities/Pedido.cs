using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class Pedido
{
    public int CodPedido { get; set; }

    public int? CodCliente { get; set; }

    public int? CodDireccion { get; set; }

    public decimal? Total { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual Cliente? CodClienteNavigation { get; set; }

    public virtual DireccionesCliente? CodDireccionNavigation { get; set; }
}
