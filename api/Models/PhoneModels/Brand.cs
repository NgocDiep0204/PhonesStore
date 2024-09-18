using System.ComponentModel.DataAnnotations;

namespace api.Models.PhoneModels
{
    public class Brand
    {
        [Key]
        public string? BrandID { get; set; }
        public string? BrandName { get; set; }
        public ICollection<Model>? Models{  get; set; }
    }
}
