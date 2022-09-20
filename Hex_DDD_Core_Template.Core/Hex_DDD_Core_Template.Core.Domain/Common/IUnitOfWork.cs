namespace Hex_DDD_Core_Template.Core.Domain.Common
{
    public interface IUnitOfWork<T> : IDisposable
        where T : class
    {
        Task<T> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task CommitTransactionAsync(T transaction, CancellationToken cancellationToken = default(CancellationToken));

        Task RollBackTransactionAsync(T transaction, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}