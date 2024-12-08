using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class DeletePostCommand : ICommand
{
    public Guid Id { get; set; }
}
