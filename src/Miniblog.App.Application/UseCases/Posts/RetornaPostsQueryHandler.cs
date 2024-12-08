using Miniblog.App.Application.Abstractions;
using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Shared;
using FluentResults;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class RetornaPostsQueryHandler(IPostRepository postRepository) : IQueryHandler<RetornaPostsQuery, IEnumerable<PostResponse>>
{
    public async Task<Result<IEnumerable<PostResponse>>> Handle(RetornaPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await postRepository.RetornaPostsAsync(cancellationToken);

        if (posts is null || posts.Count() <= 0)
            return Result.Fail(DomainErrors.NaoForamEncontradosRegistrosParaOsParametrosInformados);

        var postsResult = posts.Select(p => new PostResponse
        (
            p.Id,
            p.User.Name,
            p.Title,
            p.Description,
            p.CreatedDate,
            p.UpdatedDate
        ));

        return Result.Ok(postsResult);
    }
}