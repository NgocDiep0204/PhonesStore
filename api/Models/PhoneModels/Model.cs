using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models.PhoneModels
{
    public class Model
    {
        public string? ModelID { get; set; }
        public string? BrandID { get; set; }
        public string? ModelName { get; set; }
        public string? ModelImgUrl { get; set; }

        [ForeignKey("BrandID")]
        public Brand? Brand { get; set; }
        public ICollection<Variant>? Variants { get; set; }
    }
}
