using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class Cliente
{
    public int CodCliente { get; set; }

    public string? NombreCliente { get; set; }

    public string? Telefono { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual ICollection<DireccionesCliente> DireccionesClientes { get; set; } = new List<DireccionesCliente>();

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
