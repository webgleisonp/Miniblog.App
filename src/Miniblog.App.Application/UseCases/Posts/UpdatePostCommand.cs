using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Application.UseCases.Posts;

public sealed class UpdatePostCommand : ICommand
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}