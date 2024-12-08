namespace Miniblog.App.Application.UseCases.Posts;

public sealed record PostResponse(Guid Id, string UserName, string Title, string Description, DateTime CreatedDate, DateTime? UpdatedDate);