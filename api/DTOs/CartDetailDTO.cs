namespace api.DTOs
{
    public class CartDetailDTO
    {
        public string? CartID { get; set; }
        public string? ProductID { get; set; }
        public int? Quantity { get; set; }
        public bool? IsSelected { get; set; }
    }
}
