using Miniblog.App.Application.Abstractions;

namespace Miniblog.App.Data;
internal sealed class UnityOfWork(MiniblogDbContext dbContext) : IUnityOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
