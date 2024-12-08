namespace Miniblog.App.Application.Abstractions;

public interface IUnityOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
