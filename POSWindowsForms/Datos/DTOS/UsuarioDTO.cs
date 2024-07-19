using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Datos.DTOS
{
    public class UsuarioDTO
    {
        public int CodUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public bool Activo { get; set; }
    }
}
