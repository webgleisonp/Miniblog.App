using Miniblog.App.Domain.Shared;
using Miniblog.App.Domain.SuportTypes;
using FluentResults;

namespace Miniblog.App.Domain.Entities;

public sealed class User : Entity
{
    private User(Guid id, string name, string email, string password)
        : base(id)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public string Name { get; init; }
    public string Email { get; init; }
    public string Password { get; init; }

    public static Result<User> CreateNewUser(string name, string email, string password)
    {
        if (string.IsNullOrEmpty(name)) return Result.Fail<User>(DomainErrors.CampoObrigatorio(nameof(name)));

        if (string.IsNullOrEmpty(email)) return Result.Fail<User>(DomainErrors.CampoObrigatorio(nameof(email)));

        if (string.IsNullOrEmpty(password)) return Result.Fail<User>(DomainErrors.CampoObrigatorio(nameof(password)));

        var user = new User(Guid.NewGuid(), name, email, password);

        return user;
    }
}
