using Microsoft.AspNetCore.Identity;

namespace api.Models.AuthenticationModels
{
    public class RegisterResponse
    {
        public IdentityResult Result { get; set; }
        public IdentityUser User { get; set; }
    }
}
