using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Hex_DDD_Core_Template.Core.Domain.Common;
using Hex_DDD_Core_Template.Core.Infrastructure.EntityTypeConfigurations;
using Hex_DDD_Core_Template.Core.Domain.WeatherAggregate;
using System.Data;

namespace Hex_DDD_Core_Template.Core.Infrastructure
{
    public class ApplicationDbContext : DbContext, IUnitOfWork<IDbContextTransaction>
    {
        public const string DEFAULT_SCHEMA = "WeatherForecast";

        public DbSet<WeatherForecast> WeatherForecasts {get; set;}

        private ILogger<ApplicationDbContext> _logger;
        private IDbContextTransaction _currentTransaction;
        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        //public string DbPath { get; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger)
            : base(options)
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "blogging.db");
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new WeatherForecastEntityTypeConfiguration());
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        { }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers.
            //await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed

            return await base.SaveChangesAsync(cancellationToken) > 0 ? true : false; 
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
            _logger.LogInformation("Transaction {transactionId} begun: {@transaction}", _currentTransaction.TransactionId, _currentTransaction.GetDbTransaction());

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveEntitiesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
                _logger.LogInformation("Transaction {transactionId} commited", _currentTransaction.TransactionId);
            }
            catch
            {
                await RollBackTransactionAsync(transaction);
                _logger.LogWarning("Transaction {transactionId} could not be commited", _currentTransaction?.TransactionId);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public async Task RollBackTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogInformation("Transaction {transactionId} rolled back", _currentTransaction?.TransactionId);
            }
            catch
            {
                _logger.LogWarning("Transaction {transactionId} could not be rolled back", _currentTransaction?.TransactionId);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}