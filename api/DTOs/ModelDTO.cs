namespace api.DTOs
{
    public class ModelDTO
    {
        public string? ModelID { get; set; }
        public string? BrandID { get; set; }
        public string? ModelName { get; set; }
        public string? ModelImgUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
