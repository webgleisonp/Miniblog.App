using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Miniblog.App.Data.Repositories;

internal sealed class UserRepository(MiniblogDbContext dbContext) : IUserRepository
{
    public async ValueTask CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.AddAsync(user, cancellationToken);
    }

    public async ValueTask<bool> VerifyUserExistByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AnyAsync(p => p.Email == email);
    }

    public async ValueTask<User> FindByEmailAndPassword(string email, string password, CancellationToken cancellationToken)
    {
        return await dbContext.Users.SingleOrDefaultAsync(u=>u.Email == email && u.Password == password);
    }

    public async ValueTask<bool> VerifyUserExistByIdAsync(Guid id)
    {
        return await dbContext.Users.AnyAsync(p => p.Id == id);
    }
}
