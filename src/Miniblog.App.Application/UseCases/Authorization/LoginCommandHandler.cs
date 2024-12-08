using Miniblog.App.Application.Abstractions;
using Miniblog.App.Domain.Abstractions;
using Miniblog.App.Domain.Shared;
using FluentResults;
using MediatR;

namespace Miniblog.App.Application.UseCases.Authorization;

public sealed class LoginCommandHandler(IUserRepository repository, IJwtProvider jwtProvider) : ICommandHandler<LoginCommand, TokenResponse>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email))
            return Result.Fail(DomainErrors.CampoObrigatorio(nameof(LoginCommand.Email)));

        if (string.IsNullOrEmpty(request.Password))
            return Result.Fail(DomainErrors.CampoObrigatorio(nameof(LoginCommand.Password)));

        var user = await repository.FindByEmailAndPassword(request.Email, request.Password, cancellationToken);

        if (user is null)
            return Result.Fail(DomainErrors.DadosInvalidos);

        var token = jwtProvider.Generate(user);

        return Result.Ok(new TokenResponse(token));
    }
}