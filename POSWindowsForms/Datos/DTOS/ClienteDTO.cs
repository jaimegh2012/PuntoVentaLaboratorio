using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Datos.DTOS
{
    public class ClienteDTO
    {
        public int CodCliente { get; set; }
        public string NombreCliente { get; set; }

        public string Telefono { get; set; }

        public bool Activo { get; set; }
    }
}
