using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class Producto
{
    public int CodProducto { get; set; }

    public string? NombreProducto { get; set; }

    public decimal? Precio { get; set; }

    public int? CodCategoria { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual Categoria? CodCategoriaNavigation { get; set; }

    public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
}
