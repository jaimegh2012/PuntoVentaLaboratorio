﻿namespace POSApi.Security.Models
{
    public class AuthorizationResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Resultado { get; set; }
        public string Msg { get; set; }
    }
}
