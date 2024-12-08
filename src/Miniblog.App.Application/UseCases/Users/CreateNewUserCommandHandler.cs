using Miniblog.App.Application.Abstractions;
using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Entities;
using Miniblog.App.Domain.Shared;
using FluentResults;

namespace Miniblog.App.Application.UseCases.Users;

public sealed class CreateNewUserCommandHandler(IUnityOfWork unityOfWork, IUserRepository repository) : ICommandHandler<CreateNewUserCommand>
{
    public async Task<Result> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
    {
        var userExists = await repository.VerifyUserExistByEmailAsync(request.Email, cancellationToken);

        if (userExists)
            return Result.Fail(DomainErrors.UsuarioJaExiste);

        var newUserResult = User.CreateNewUser(request.Name, request.Email, request.Password);

        if (newUserResult.IsFailed)
            return Result.Fail(newUserResult.Errors);

        await repository.CreateUserAsync(newUserResult.Value, cancellationToken);

        await unityOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}