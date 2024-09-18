using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
namespace api.Models.PhoneModels

{
    public class Cart
    {
        [Key]
        public string? CartID { get; set; }
        public string? UserId { get; set; }
        public decimal? TotalPrice { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser? identityUser { get; set; }
        public ICollection<CartDetail>? CartDetails { get; set; }
    }
}
    