using Content.Domain.Entities;

namespace Content.Domain.Repositories;

public interface IContentRepository
{
    Task<Entities.Content?> GetByIdAsync(string contentId);
    Task UpdateAsync(Entities.Content content);
}