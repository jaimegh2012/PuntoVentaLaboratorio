using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Datos.DTOS
{
    public class DireccionClienteDTO
    {
        public int? CodDireccion { get; set; }
        public int? CodCliente { get; set; }

        public string Direccion { get; set; }

        public bool? Activo { get; set; }
    }
}
