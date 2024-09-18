using api.DTOs;
using api.Function;
using api.Models.AuthenticationModels;
using Microsoft.AspNetCore.Identity; 
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace api.Services
{
    public interface IAuthencationService
    {
        Task<ServiceResponse.GeneralResponse> RegisterAsync(RegisterRequest request);
        Task<ServiceResponse.LoginResponse> LoginAsync(LoginRequest request);
        Task<ApiResponse<List<string>>> AssiignRoleToUserAsync(IEnumerable<string> roles, IdentityUser user);
        Task<IActionResult> ConfirmEmail(string token, string email);
        public JwtSecurityToken GetToken(List<Claim> authClaims);
        public string GenerateRandomOTP();
        Task RemoveExpiredOtps();

    }
}