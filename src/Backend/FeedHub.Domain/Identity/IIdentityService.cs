using FeedHub.Domain.Models;

namespace FeedHub.Domain.Identity;

public interface IIdentityService
{
    Task CreateUser(string name, string email, string password);
    string CreateJwtToken(string userId, string email);
    Task<User?> IsAnUserValid(string email, string password);
}
