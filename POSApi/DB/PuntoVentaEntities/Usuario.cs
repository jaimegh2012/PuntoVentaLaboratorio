using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class Usuario
{
    public int CodUsuario { get; set; }

    public string? NombreUsuario { get; set; }

    public string? Usuario1 { get; set; }

    public string? Clave { get; set; }

    public bool? Activo { get; set; }

    public bool? Eliminado { get; set; }

    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; set; } = new List<HistorialRefreshToken>();
}
