using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Application.UseCases.Authorization;

public sealed class LoginCommand : ICommand<TokenResponse>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}