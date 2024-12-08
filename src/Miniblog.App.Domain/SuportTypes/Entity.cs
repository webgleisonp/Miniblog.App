namespace Miniblog.App.Domain.SuportTypes;

public class Entity
{
    public Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}
