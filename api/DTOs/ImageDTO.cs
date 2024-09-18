namespace api.DTOs
{
    public class ImageDTO
    {
        public string? ImageID { get; set; }
        public string? ProductID { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }
    }
}
