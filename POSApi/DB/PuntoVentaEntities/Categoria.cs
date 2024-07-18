using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class Categoria
{
    public int CodCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
