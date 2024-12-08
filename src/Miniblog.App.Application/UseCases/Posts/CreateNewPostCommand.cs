using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class CreateNewPostCommand : ICommand
{
    public Guid UserId { get; set; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}