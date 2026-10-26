using FeedHub.Domain.Repositories;
using FeedHub.Infrastructure.Data.Context;

namespace FeedHub.Infrastructure.Data.Repositories;

internal class UnitOfWork : IUnitOfWork
{
    private readonly ApiDbContext _context;

    public UnitOfWork(ApiDbContext context) => _context = context;

    public async Task Commit() => await _context.SaveChangesAsync();
}
