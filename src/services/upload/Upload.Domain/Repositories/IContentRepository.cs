using Upload.Domain.Entities;

namespace Upload.Domain.Repositories;

public interface IContentRepository
{
    Task CreateAsync(Content content);
    Task<Content?> GetByIdAsync(string contentId);
    Task UpdateAsync(Content content);
}