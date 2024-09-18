using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.PhoneModels
{
    public class Product
    {
        [Key]
        public string? ProductID { get; set; }
        public string? VariantID { get; set; }
        public string? ColorID { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
        [ForeignKey("VariantID")]
        public Variant? Variant { get; set; }
        [ForeignKey("ColorID")]
        public Color? Color { get; set; }
        public ICollection<Image>? Images { get; set; }
    }
}
