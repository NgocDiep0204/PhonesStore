namespace api.DTOs
{
    public class ProductDTO
    {
        public string? ProductID { get; set; }
        public string? VariantID { get; set; }
        public string? ColorID { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string? Description { get; set; }
    }
}
