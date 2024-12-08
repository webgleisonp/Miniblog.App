using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Application.UseCases.Users;

public sealed class CreateNewUserCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
