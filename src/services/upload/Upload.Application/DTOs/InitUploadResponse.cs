namespace Upload.Application.DTOs;

public class InitUploadResponse
{
    public string ContentId { get; set; } = string.Empty;
    public string UploadUrl { get; set; } = string.Empty;
    public int ExpiresIn { get; set; } = 900;
    public bool IsDuplicate { get; set; }
}