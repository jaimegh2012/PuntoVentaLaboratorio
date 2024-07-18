﻿using System;
using System.Collections.Generic;

namespace POSApi.DB.PuntoVentaEntities;

public partial class HistorialRefreshToken
{
    public int CodHistorialToken { get; set; }

    public int? CodUsuario { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? EsActivo { get; set; }

    public virtual Usuario? CodUsuarioNavigation { get; set; }
}
