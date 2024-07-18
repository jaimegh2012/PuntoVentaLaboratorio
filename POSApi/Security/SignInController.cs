using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSApi.Security.Interfaces;
using POSApi.Security.Models;
using System.IdentityModel.Tokens.Jwt;

namespace POSApi.Security
{

    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;

        public SignInController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("Autenticar")]
        public async Task<IActionResult> Autenticar(AuthorizationRequest authorizationRequest)
        {
            var resultado_autorizacion = await _authorizationService.DevolverToken(authorizationRequest);

            if (resultado_autorizacion == null)
                return Unauthorized();

            return Ok(resultado_autorizacion);
        }

        [HttpPost("ObtenerRefreshToken")]
        public async Task<IActionResult> ObtenerRefreshToken(RefreshTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenExpiradoSupuestamente = tokenHandler.ReadJwtToken(request.TokenExpirado);

            if (tokenExpiradoSupuestamente.ValidTo > DateTime.UtcNow)
                return BadRequest(new AuthorizationResponse() { Resultado = false, Msg = "Token no ha expirado" });


            var codUsuario = tokenExpiradoSupuestamente.Claims.First(x =>
            x.Type == JwtRegisteredClaimNames.NameId).Value.ToString();


            var autorizacionResponse = await _authorizationService.DevolverRefreshToken(request, int.Parse(codUsuario));

            if (autorizacionResponse.Resultado)
                return Ok(autorizacionResponse);

            return BadRequest(autorizacionResponse);
        }
    }
}
