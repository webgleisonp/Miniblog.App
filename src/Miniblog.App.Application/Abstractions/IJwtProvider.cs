using Miniblog.App.Domain.Entities;

namespace Miniblog.App.Application.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}