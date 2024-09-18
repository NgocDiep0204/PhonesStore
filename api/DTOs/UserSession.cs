namespace api.DTOs
{
    //tạo 1 bản ghi UserSession dành cho ng dùng có id, role, token
    public record UserSession(string Id, string Role, string UserName string Token);
}
