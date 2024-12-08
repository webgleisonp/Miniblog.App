using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Miniblog.App.Data.Repositories;

internal sealed class PostRepository(MiniblogDbContext dbContext) : IPostRepository
{
    public async ValueTask CreateNewPostAsync(Post post, CancellationToken cancellationToken)
    {
        await dbContext.Posts.AddAsync(post, cancellationToken);
    }

    public ValueTask DeletePostAsync(Post post, CancellationToken cancellationToken)
    {
        dbContext.Posts.Remove(post);

        return ValueTask.CompletedTask;
    }

    public async ValueTask<Post> GetPostByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Posts.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async ValueTask<IEnumerable<Post>> RetornaPostsAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Posts
            .Include(p => p.User)
            .AsSplitQuery()
            .ToListAsync(cancellationToken);
    }

    public ValueTask UpdatePostAsync(Post post, CancellationToken cancellationToken)
    {
        dbContext.Posts.Update(post);

        return ValueTask.CompletedTask;
    }
}
