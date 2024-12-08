using Miniblog.App.Application.Abstractions;
using Miniblog.App.Application.WebSocket;
using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Entities;
using Miniblog.App.Domain.Shared;
using FluentResults;
using Microsoft.AspNetCore.SignalR;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class CreateNewPostCommandHandler(IUnityOfWork unityOfWork, IHubContext<NotificationHub, INotificationClient> hubContext, IPostRepository postRepository, IUserRepository userRepository) : ICommandHandler<CreateNewPostCommand>
{
    public async Task<Result> Handle(CreateNewPostCommand request, CancellationToken cancellationToken)
    {
        var userExists = await userRepository.VerifyUserExistByIdAsync(request.UserId);

        if (!userExists)
            return Result.Fail(DomainErrors.UsuarioNaoCadastrado);

        var newPostResult = Post.CreateNewPost(request.UserId, request.Title, request.Description);

        if (newPostResult.IsFailed)
            return Result.Fail(newPostResult.Errors);

        await postRepository.CreateNewPostAsync(newPostResult.Value, cancellationToken);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        await hubContext.Clients.All.ReceiveNotification($"Novo post {newPostResult.Value.Title} criado em {DateTime.UtcNow.ToString()}");

        return Result.Ok();
    }
}
