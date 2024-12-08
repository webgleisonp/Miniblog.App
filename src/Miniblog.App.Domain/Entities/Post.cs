using Miniblog.App.Domain.Shared;
using Miniblog.App.Domain.SuportTypes;
using FluentResults;

namespace Miniblog.App.Domain.Entities;

public sealed class Post : Entity
{
    private Post(Guid id, Guid userId, string title, string description, DateTime createdDate)
        : base(id)
    {
        UserId = userId;
        Title = title;
        Description = description;
        CreatedDate = createdDate;
    }

    public Guid UserId { get; private set; }
    public User User { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? UpdatedDate { get; private set; }

    public static Result<Post> CreateNewPost(Guid userId, string title, string description)
    {
        if (userId == Guid.Empty) return Result.Fail(DomainErrors.CampoObrigatorio(nameof(userId)));

        if (string.IsNullOrEmpty(title)) return Result.Fail(DomainErrors.CampoObrigatorio(nameof(title)));

        if (string.IsNullOrEmpty(description)) return Result.Fail(DomainErrors.CampoObrigatorio(nameof(description)));

        var newPost = new Post(Guid.NewGuid(), userId, title, description, DateTime.UtcNow);

        return newPost;
    }

    public Result SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title)) return Result.Fail(DomainErrors.CampoObrigatorio(nameof(title)));

        Title = title;

        return Result.Ok();
    }

    public Result SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description)) return Result.Fail(DomainErrors.CampoObrigatorio(nameof(description)));

        Description = description;

        return Result.Ok();
    }

    public Result SetUpdatedDate(DateTime updatedDate)
    {
        if (updatedDate < CreatedDate)
            return Result.Fail(DomainErrors.DataAtualizacaoMenorDataCriacao);

        UpdatedDate = updatedDate;

        return Result.Ok();
    }
}
