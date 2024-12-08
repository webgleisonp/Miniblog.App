using Miniblog.App.Domain.Entities;

namespace Miniblog.App.Domain.Abstractions;

public interface IPostRepository
{
    ValueTask CreateNewPostAsync(Post post, CancellationToken cancellationToken);
    ValueTask<IEnumerable<Post>> RetornaPostsAsync(CancellationToken cancellationToken);
    ValueTask<Post> GetPostByIdAsync(Guid id, CancellationToken cancellationToken);
    ValueTask UpdatePostAsync(Post post, CancellationToken cancellationToken);
    ValueTask DeletePostAsync(Post post, CancellationToken cancellationToken);
}
