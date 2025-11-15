namespace Upload.Application.DTOs;

public class InitUploadRequest
{
    public string Type { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long ContentLength { get; set; }
    public string ContentHash { get; set; } = string.Empty;
}