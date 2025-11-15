using Upload.Domain.Entities;

namespace Upload.Domain.Repositories;

public interface IContentRepository
{
    Task CreateAsync(Content content);
}