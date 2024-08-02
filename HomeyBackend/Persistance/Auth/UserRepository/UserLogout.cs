﻿using System.Security.Claims;

namespace HomeyBackend.Persistance.Auth.UserRepository
{
    public partial class UserService
    {
        public async Task<AppResponse<bool>> UserLogoutAsync(ClaimsPrincipal user)
        {
            if (user.Identity?.IsAuthenticated ?? false)
            {
                var username = user.Claims.First(x => x.Type == "UserName").Value;
                var appUser = _context.Users.First(x => x.UserName == username);
                if (appUser != null) { await _userManager.UpdateSecurityStampAsync(appUser); }
                return new AppResponse<bool>().SetSuccessResponse(true);
            }
            return new AppResponse<bool>().SetSuccessResponse(true);
        }
    }
}
