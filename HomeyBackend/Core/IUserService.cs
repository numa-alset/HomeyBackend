using System.Security.Claims;
using HomeyBackend.Persistance.Auth;
using HomeyBackend.Persistance.Auth.UserRepository;

namespace HomeyBackend.Core
{
    public interface IUserService
    {
        Task<AppResponse<UserLoginResponse>> UserLoginAsync(UserLoginRequest request);
        Task<AppResponse<bool>> UserLogoutAsync(ClaimsPrincipal user);
        Task<AppResponse<UserRefreshTokenResponse>> UserRefreshTokenAsync(UserRefreshTokenRequest request);
        Task<AppResponse<bool>> UserRegisterAdminAsync(UserRegisterRequest request);
        Task<AppResponse<bool>> UserRegisterAsync(UserRegisterRequest request);
    }
}