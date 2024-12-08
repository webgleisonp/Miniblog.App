using Miniblog.App.Application.Abstractions;
using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Shared;
using FluentResults;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class DeletePostCommandHandler(IUnityOfWork unityOfWork, IPostRepository postRepository) : ICommandHandler<DeletePostCommand>
{
    public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var postExists = await postRepository.GetPostByIdAsync(request.Id, cancellationToken);

        if (postExists is null)
            return Result.Fail(DomainErrors.NaoForamEncontradosRegistrosParaOsParametrosInformados);

        await postRepository.DeletePostAsync(postExists, cancellationToken);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}