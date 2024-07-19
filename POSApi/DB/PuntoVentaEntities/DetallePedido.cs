using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class DetallePedido
{
    public int CodDetallePedido { get; set; }

    public int? CodProducto { get; set; }

    public int? CodPedido { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual Pedido? CodPedidoNavigation { get; set; }

    public virtual Producto? CodProductoNavigation { get; set; }
}
