using POSApi.Security.Models;

namespace POSApi.Security.Interfaces
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> DevolverToken(AuthorizationRequest authorization);
        Task<AuthorizationResponse> DevolverRefreshToken(RefreshTokenRequest refreshTokenRequest, int codUsuario);
    }
}
