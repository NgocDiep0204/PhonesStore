using System.ComponentModel.DataAnnotations;

namespace api.Models.AuthenticationModels
{
    public class RegisterRequest
    {
        public string Email { get; set; }
//[DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Name { get; set; }
        public string Role { get; set; }
    }
}
