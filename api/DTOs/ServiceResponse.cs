using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.DTOs
{
    public class ServiceResponse
    {
        public record class GeneralResponse (bool Flag, string Message, string token, IdentityUser user);
        public record class LoginResponse(bool Flag, string Message, string Token);
    }
}
