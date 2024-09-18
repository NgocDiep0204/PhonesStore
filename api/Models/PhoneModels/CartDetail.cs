using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.PhoneModels
{
    public class CartDetail
    {
        [Key]
        public string? CartID { get; set; }
        public string? ProductID { get; set; }
        public int? Quantity { get; set; }
        public bool? IsSelected { get; set; }
        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
        public Cart? Cart { get; set; }


    }
}
