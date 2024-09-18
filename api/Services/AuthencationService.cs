using api.Data;
using api.Models.AuthenticationModels;
//using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Function;
using api.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.ComponentModel.DataAnnotations;

namespace api.Services
{
    public class AuthencationService : IAuthencationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthencationService(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse<List<string>>> AssiignRoleToUserAsync(IEnumerable<string> roles, IdentityUser user)
        {
            var assignedRoles = new List<string>();
            foreach (var role in roles)
            {
                if (await _roleManager.RoleExistsAsync(role))
                {
                    if(await _userManager.IsInRoleAsync(user, role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                        assignedRoles.Add(role);
                    }
                }
            }
            return new ApiResponse<List<string>> { IsSuccess = true, StatusCode=200, Message="role has been assigned",
                    Response=assignedRoles};
        }

        public async Task<ServiceResponse.GeneralResponse> RegisterAsync(RegisterRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null) return new ServiceResponse.GeneralResponse(false, "Email already exists", null, null);

            if (request is null) return new ServiceResponse.GeneralResponse(false, "Request is null", null, null);
            var newUser = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email,
                Id = Guid.NewGuid().ToString()
            };
            if (await _roleManager.RoleExistsAsync(request.Role))
            {
                var result = await _userManager.CreateAsync(newUser, request.Password);
                if (!result.Succeeded) return new ServiceResponse.GeneralResponse(false, "User creation failed", null, null);
                await _userManager.AddToRoleAsync(newUser, request.Role);
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                return new ServiceResponse.GeneralResponse(true, "User created successfully", token, newUser);
            }
            return new ServiceResponse.GeneralResponse(false, "Role does not exist", null!, null!);

        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSiginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMonths(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSiginKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }

        public async Task<ServiceResponse.LoginResponse> LoginAsync(LoginRequest request)
        {
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser == null) return new ServiceResponse.LoginResponse(false, "User doesnt not exist", null!);
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingUser.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            var userRoles = await _userManager.GetRolesAsync(existingUser);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var jwttoken = GetToken(authClaims);
            var token = new JwtSecurityTokenHandler().WriteToken(jwttoken);
            return new ServiceResponse.LoginResponse(true, "logined", token);
        }

        public Task<IActionResult> ConfirmEmail(string token, string email)
        {
            throw new NotImplementedException();
        }

       public string GenerateRandomOTP()
        {
            Random generator = new Random();
            String otp = generator.Next(0, 999999).ToString("D6");
            return otp;
        }

        public async Task RemoveExpiredOtps()
        {
            var currentTime = DateTime.UtcNow;
            await _context.OtpStorages
                .Where(o => o.ExpiryTime < currentTime)
                .ExecuteDeleteAsync();
        }
    }
}
        
