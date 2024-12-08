using Miniblog.App.Application.Abstractions;
using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Shared;
using FluentResults;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class UpdatePostCommandHandler(IUnityOfWork unityOfWork, IPostRepository postRepository) : ICommandHandler<UpdatePostCommand>
{
    public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var postExists = await postRepository.GetPostByIdAsync(request.Id, cancellationToken);

        if (postExists is null)
            return Result.Fail(DomainErrors.NaoForamEncontradosRegistrosParaOsParametrosInformados);

        if(postExists.UserId != request.UserId)
            return Result.Fail(DomainErrors.EstePostNaoPertenceAoUsuarioInformado);

        var titleUpdatedResult = postExists.SetTitle(request.Title);

        if (titleUpdatedResult.IsFailed)
            return Result.Fail(titleUpdatedResult.Errors);

        var desctiptionUpdatedResult = postExists.SetDescription(request.Description);

        if (desctiptionUpdatedResult.IsFailed)
            return Result.Fail(desctiptionUpdatedResult.Errors);

        var updatedDateResult = postExists.SetUpdatedDate(DateTime.UtcNow);

        if (updatedDateResult.IsFailed)
            return Result.Fail(updatedDateResult.Errors);

        await postRepository.UpdatePostAsync(postExists, cancellationToken);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}