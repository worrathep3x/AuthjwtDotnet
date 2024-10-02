namespace Application.DTO.Response;

public class GetUserAuthenResponseDTO
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? Actor { get; set; }
    public string? Group { get; set; }
    public string? Token { get; set; }
}
public class GetAdminAuthenResponse
{
    public string? Username { get; set; }
    public string? Lastname { get; set; }
    public string? admin_email { get; set; }
    public string? Token { get; set; }
}