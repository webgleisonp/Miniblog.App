using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class RetornaPostsQuery : IQuery<IEnumerable<PostResponse>>
{
}