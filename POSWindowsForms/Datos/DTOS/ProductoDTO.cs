using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Datos.DTOS
{
    public class ProductoDTO
    {
        public int? CodProducto { get; set; }
        public string NombreProducto { get; set; }

        public decimal? Precio { get; set; }

        public int? CodCategoria { get; set; }

        public bool? Activo { get; set; }
    }
}
