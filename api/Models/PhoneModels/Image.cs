using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.PhoneModels
{
    public class Image
    {
        [Key]
        public string? ImageID { get; set; }
        public string? ProductID { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
        [ForeignKey("ProductID")]
        public Product? Product { get; set; }
    }
}   
