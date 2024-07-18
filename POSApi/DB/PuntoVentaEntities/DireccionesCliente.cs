using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class DireccionesCliente
{
    public int CodDireccion { get; set; }

    public int? CodCliente { get; set; }

    public string? Direccion { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual Cliente? CodClienteNavigation { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
