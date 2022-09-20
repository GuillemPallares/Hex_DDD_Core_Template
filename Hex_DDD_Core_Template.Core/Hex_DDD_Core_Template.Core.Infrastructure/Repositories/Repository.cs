using Hex_DDD_Core_Template.Core.Domain.Common;
using Hex_DDD_Core_Template.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

public abstract class Repository<T>
        where T : Entity
    {
        protected ILogger<Repository<T>> _logger;

        private readonly ApplicationDbContext _context;

        protected DbSet<T> dBSet { get; set; }

        protected IUnitOfWork<IDbContextTransaction> UnitOfWork => _context;


        public Repository(ApplicationDbContext context, ILogger<Repository<T>> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            dBSet = _context.Set<T>();
        }

        protected async Task<R> CancelableTransaction<R>(string action, Func < IDbContextTransaction,Task<R>> func, CancellationToken cancellationToken = default)
        {
            using (var transaction = await UnitOfWork.BeginTransactionAsync())
            {
                _logger.LogInformation("Transaction {transactionId} - {action}", transaction.TransactionId, action);

                var result = await func(transaction);

                if (cancellationToken.CanBeCanceled && cancellationToken.IsCancellationRequested)
                    await UnitOfWork.RollBackTransactionAsync(transaction, cancellationToken);

                if(!cancellationToken.IsCancellationRequested)
                    await UnitOfWork.CommitTransactionAsync(transaction, cancellationToken);

                return result;
            }
        }

        protected async Task SavePointAsync(IDbContextTransaction transaction, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (!transaction.SupportsSavepoints)
                throw new NotSupportedException("Savepoints");

            await transaction.CreateSavepointAsync(name, cancellationToken);
            _logger.LogInformation("Transaction {transactionId} - {name}",transaction.TransactionId, name);


        }
    }