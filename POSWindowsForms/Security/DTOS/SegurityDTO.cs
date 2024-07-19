using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Security.DTOS
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Resultado { get; set; }
        public string Msg { get; set; }
    }

    public class AuthorizationRequest
    {
        public string User { get; set; }
        public string Password { get; set; }
    }
}
