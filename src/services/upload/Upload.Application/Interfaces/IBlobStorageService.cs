namespace Upload.Application.Interfaces;

public interface IBlobStorageService
{
    Task<string> GenerateUploadUrlAsync(string fileName, string contentId);
}