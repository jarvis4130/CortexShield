namespace Upload.Application.Interfaces;

public interface IDuplicateCheckService
{
    Task<bool> CheckDuplicateAsync(string contentHash);
    Task SetDuplicateAsync(string contentHash);
}