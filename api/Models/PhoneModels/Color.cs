using System.ComponentModel.DataAnnotations;

namespace api.Models.PhoneModels
{
    public class Color
    {
        [Key]
        public string ? ColorID { get; set; }
        public string ? ColorName { get; set; }
        public ICollection<Product>? Products { get; set; }

    }
}
