namespace Miniblog.App.Application.UseCases.Posts;

public sealed record UpdatePostRequest(Guid UserId, string Title, string Description);
