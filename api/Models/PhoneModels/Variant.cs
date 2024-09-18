using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.PhoneModels
{
    public class Variant
    {
        [Key]
        public string? VariantID { get; set; }
        public string? ModelID { get; set; }
        public string? VariantName { get; set; }
        [ForeignKey("ModelID")]
        public Model? Model { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
