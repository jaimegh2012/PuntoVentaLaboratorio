﻿namespace POSApi.Security.Models
{
    public class RefreshTokenRequest
    {
        public string TokenExpirado { get; set; }
        public string RefreshToken { get; set; }
    }
}
