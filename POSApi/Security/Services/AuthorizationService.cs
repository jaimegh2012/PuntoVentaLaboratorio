using Microsoft.IdentityModel.Tokens;
using POSApi.DB.PuntoVentaEntities;
using POSApi.Security.Interfaces;
using POSApi.Security.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace POSApi.Security.Services
{
    public class AuthorizationService: IAuthorizationService
    {
        private readonly PuntoVentaEntities _db = new();
        private IConfiguration _configuration;

        public AuthorizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AuthorizationResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int codUsuario)
        {
            var refreshTokenEncontrado = _db.HistorialRefreshTokens.FirstOrDefault(x => x.Token == refreshTokenRequest.TokenExpirado &&
            x.RefreshToken == refreshTokenRequest.RefreshToken && x.CodUsuario == codUsuario && x.EsActivo == true);

            if (refreshTokenEncontrado == null)
                return new AuthorizationResponse() { Resultado = false, Msg = "No existe el refreshToken" };

            var usuario = _db.Usuarios.FirstOrDefault(x => x.CodUsuario == codUsuario);

            var token = GenerarToken(usuario);
            var refreshToken = GenerarRefreshToken();

            return await GuardarHistorialRefreshToken(codUsuario, token, refreshToken);
        }

        public async Task<AuthorizationResponse> DevolverToken(AuthorizationRequest authorization)
        {
            var usuarioEncontrado = _db.Usuarios
                .FirstOrDefault(x => x.Usuario1 == authorization.User && x.Clave == authorization.Password);

            if (usuarioEncontrado == null)
                return await Task.FromResult<AuthorizationResponse>(null);


            var token = GenerarToken(usuarioEncontrado);

            var refreshToken = GenerarRefreshToken();


            //AuthorizationResponse response = new AuthorizationResponse()
            //{
            //    Token = token,
            //    Resultado = true,
            //    Msg = "Ok"
            //};

            //return response;

            return await GuardarHistorialRefreshToken(usuarioEncontrado.CodUsuario, token, refreshToken);
        }

        public string GenerarToken(Usuario usuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.CodUsuario.ToString()));

            SigningCredentials credencialesToken = new(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credencialesToken
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenConfig);
        }

        public string GenerarRefreshToken()
        {
            var byteArray = new byte[64];
            var refreshToken = "";

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                refreshToken = Convert.ToBase64String(byteArray);
            }

            return refreshToken;
        }

        private async Task<AuthorizationResponse> GuardarHistorialRefreshToken(int codUsuario, string token, string refreshToken)
        {
            HistorialRefreshToken historialRefreshToken = new()
            {
                CodUsuario = codUsuario,
                Token = token,
                RefreshToken = refreshToken,
                FechaCreacion = DateTime.UtcNow,
                FechaExpiracion = DateTime.UtcNow.AddDays(5),

            };

            await _db.HistorialRefreshTokens.AddAsync(historialRefreshToken);
            await _db.SaveChangesAsync();

            return new AuthorizationResponse() { Token = token, RefreshToken = refreshToken, Resultado = true, Msg = "OK" };
        }

    }
}
