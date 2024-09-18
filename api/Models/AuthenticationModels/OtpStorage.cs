using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace api.Models.AuthenticationModels
{
    public class OtpStorage
    { 
        [Key]
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Otp { get; set; }
        public DateTime ExpiryTime { get; set; }
        public IdentityUser? identityUser { get; set; }
    }
}
