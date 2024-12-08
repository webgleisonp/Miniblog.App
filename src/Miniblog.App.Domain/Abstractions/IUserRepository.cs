using Miniblog.App.Domain.Entities;

namespace Miniblog.App.Domain.Abstractions;

public interface IUserRepository
{
    ValueTask CreateUserAsync(User user, CancellationToken cancellationToken);
    ValueTask<bool> VerifyUserExistByEmailAsync(string email, CancellationToken cancellationToken);
    ValueTask<User> FindByEmailAndPassword(string email, string password, CancellationToken cancellationToken);
    ValueTask<bool> VerifyUserExistByIdAsync(Guid id);
}
