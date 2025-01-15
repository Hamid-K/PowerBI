using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Sql;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000262 RID: 610
	public class CommitFailureHandler : TransactionHandler
	{
		// Token: 0x06001EF4 RID: 7924 RVA: 0x00055ED1 File Offset: 0x000540D1
		public CommitFailureHandler()
			: this((DbConnection c) => new TransactionContext(c))
		{
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00055EF8 File Offset: 0x000540F8
		public CommitFailureHandler(Func<DbConnection, TransactionContext> transactionContextFactory)
		{
			Check.NotNull<Func<DbConnection, TransactionContext>>(transactionContextFactory, "transactionContextFactory");
			this._transactionContextFactory = transactionContextFactory;
			this.Transactions = new Dictionary<DbTransaction, TransactionRow>();
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x00055F29 File Offset: 0x00054129
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x00055F31 File Offset: 0x00054131
		protected internal TransactionContext TransactionContext { get; private set; }

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001EF8 RID: 7928 RVA: 0x00055F3A File Offset: 0x0005413A
		// (set) Token: 0x06001EF9 RID: 7929 RVA: 0x00055F42 File Offset: 0x00054142
		private protected Dictionary<DbTransaction, TransactionRow> Transactions { protected get; private set; }

		// Token: 0x06001EFA RID: 7930 RVA: 0x00055F4B File Offset: 0x0005414B
		protected virtual IDbExecutionStrategy GetExecutionStrategy()
		{
			return null;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00055F50 File Offset: 0x00054150
		public override void Initialize(ObjectContext context)
		{
			base.Initialize(context);
			DbConnection storeConnection = ((EntityConnection)base.ObjectContext.Connection).StoreConnection;
			this.Initialize(storeConnection);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00055F81 File Offset: 0x00054181
		public override void Initialize(DbContext context, DbConnection connection)
		{
			base.Initialize(context, connection);
			this.Initialize(connection);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00055F94 File Offset: 0x00054194
		private void Initialize(DbConnection connection)
		{
			DbContextInfo currentInfo = DbContextInfo.CurrentInfo;
			DbContextInfo.CurrentInfo = null;
			try
			{
				this.TransactionContext = this._transactionContextFactory(connection);
				if (this.TransactionContext != null)
				{
					this.TransactionContext.Configuration.LazyLoadingEnabled = false;
					this.TransactionContext.Configuration.AutoDetectChangesEnabled = false;
					this.TransactionContext.Database.Initialize(false);
				}
			}
			finally
			{
				DbContextInfo.CurrentInfo = currentInfo;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00056014 File Offset: 0x00054214
		protected virtual int PruningLimit
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x00056018 File Offset: 0x00054218
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed && disposing && this.TransactionContext != null)
			{
				if (this._rowsToDelete.Any<TransactionRow>())
				{
					try
					{
						this.PruneTransactionHistory(true, false);
					}
					catch (Exception)
					{
					}
				}
				this.TransactionContext.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x00056078 File Offset: 0x00054278
		public override string BuildDatabaseInitializationScript()
		{
			if (this.TransactionContext != null)
			{
				IEnumerable<MigrationStatement> enumerable = TransactionContextInitializer<TransactionContext>.GenerateMigrationStatements(this.TransactionContext);
				StringBuilder stringBuilder = new StringBuilder();
				MigratorScriptingDecorator.BuildSqlScript(enumerable, stringBuilder);
				return stringBuilder.ToString();
			}
			return null;
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x000560AC File Offset: 0x000542AC
		public override void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
			if (this.TransactionContext == null || !this.MatchesParentContext(connection, interceptionContext) || interceptionContext.Result == null)
			{
				return;
			}
			Guid transactionId = Guid.NewGuid();
			bool flag = false;
			bool flag2 = false;
			ObjectContext objectContext = ((IObjectContextAdapter)this.TransactionContext).ObjectContext;
			((EntityConnection)objectContext.Connection).UseStoreTransaction(interceptionContext.Result);
			while (!flag)
			{
				TransactionRow transactionRow = new TransactionRow
				{
					Id = transactionId,
					CreationTime = DateTime.Now
				};
				this.Transactions.Add(interceptionContext.Result, transactionRow);
				this.TransactionContext.Transactions.Add(transactionRow);
				try
				{
					objectContext.SaveChangesInternal(SaveOptions.AcceptAllChangesAfterSave, true);
					flag = true;
				}
				catch (UpdateException)
				{
					this.Transactions.Remove(interceptionContext.Result);
					this.TransactionContext.Entry<TransactionRow>(transactionRow).State = EntityState.Detached;
					if (flag2)
					{
						throw;
					}
					try
					{
						if (this.TransactionContext.Transactions.AsNoTracking<TransactionRow>().WithExecutionStrategy(new DefaultExecutionStrategy()).FirstOrDefault((TransactionRow t) => t.Id == transactionId) == null)
						{
							throw;
						}
						transactionId = Guid.NewGuid();
					}
					catch (EntityCommandExecutionException)
					{
						this.TransactionContext.Database.Initialize(true);
						flag2 = true;
					}
				}
			}
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00056270 File Offset: 0x00054470
		public override void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			TransactionRow transactionRow;
			if (this.TransactionContext == null || (interceptionContext.Connection != null && !this.MatchesParentContext(interceptionContext.Connection, interceptionContext)) || !this.Transactions.TryGetValue(transaction, out transactionRow))
			{
				return;
			}
			this.Transactions.Remove(transaction);
			if (interceptionContext.Exception == null)
			{
				this.PruneTransactionHistory(transactionRow);
				return;
			}
			TransactionRow transactionRow2 = null;
			bool suspended = DbExecutionStrategy.Suspended;
			try
			{
				DbExecutionStrategy.Suspended = false;
				IDbExecutionStrategy dbExecutionStrategy = this.GetExecutionStrategy() ?? DbProviderServices.GetExecutionStrategy(interceptionContext.Connection);
				transactionRow2 = this.TransactionContext.Transactions.AsNoTracking<TransactionRow>().WithExecutionStrategy(dbExecutionStrategy).SingleOrDefault((TransactionRow t) => t.Id == transactionRow.Id);
			}
			catch (EntityCommandExecutionException)
			{
			}
			finally
			{
				DbExecutionStrategy.Suspended = suspended;
			}
			if (transactionRow2 != null)
			{
				interceptionContext.Exception = null;
				this.PruneTransactionHistory(transactionRow);
				return;
			}
			this.TransactionContext.Entry<TransactionRow>(transactionRow).State = EntityState.Detached;
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000563F8 File Offset: 0x000545F8
		public override void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			TransactionRow transactionRow;
			if (this.TransactionContext == null || (interceptionContext.Connection != null && !this.MatchesParentContext(interceptionContext.Connection, interceptionContext)) || !this.Transactions.TryGetValue(transaction, out transactionRow))
			{
				return;
			}
			this.Transactions.Remove(transaction);
			this.TransactionContext.Entry<TransactionRow>(transactionRow).State = EntityState.Detached;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00056454 File Offset: 0x00054654
		public override void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
			this.RolledBack(transaction, interceptionContext);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00056460 File Offset: 0x00054660
		public virtual void ClearTransactionHistory()
		{
			foreach (TransactionRow transactionRow in this.TransactionContext.Transactions)
			{
				this.MarkTransactionForPruning(transactionRow);
			}
			this.PruneTransactionHistory(true, true);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000564BC File Offset: 0x000546BC
		public Task ClearTransactionHistoryAsync()
		{
			return this.ClearTransactionHistoryAsync(CancellationToken.None);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000564CC File Offset: 0x000546CC
		public virtual async Task ClearTransactionHistoryAsync(CancellationToken cancellationToken)
		{
			await this.TransactionContext.Transactions.ForEachAsync(new Action<TransactionRow>(this.MarkTransactionForPruning), cancellationToken).WithCurrentCulture();
			await this.PruneTransactionHistoryAsync(true, true, cancellationToken).WithCurrentCulture();
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x00056519 File Offset: 0x00054719
		protected virtual void MarkTransactionForPruning(TransactionRow transaction)
		{
			Check.NotNull<TransactionRow>(transaction, "transaction");
			if (!this._rowsToDelete.Contains(transaction))
			{
				this._rowsToDelete.Add(transaction);
			}
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00056542 File Offset: 0x00054742
		public void PruneTransactionHistory()
		{
			this.PruneTransactionHistory(true, true);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0005654C File Offset: 0x0005474C
		public Task PruneTransactionHistoryAsync()
		{
			return this.PruneTransactionHistoryAsync(CancellationToken.None);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00056559 File Offset: 0x00054759
		public Task PruneTransactionHistoryAsync(CancellationToken cancellationToken)
		{
			return this.PruneTransactionHistoryAsync(true, true, cancellationToken);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x00056564 File Offset: 0x00054764
		protected virtual void PruneTransactionHistory(bool force, bool useExecutionStrategy)
		{
			if (this._rowsToDelete.Count > 0 && (force || this._rowsToDelete.Count > this.PruningLimit))
			{
				foreach (TransactionRow transactionRow in this.TransactionContext.Transactions.ToList<TransactionRow>())
				{
					if (this._rowsToDelete.Contains(transactionRow))
					{
						this.TransactionContext.Transactions.Remove(transactionRow);
					}
				}
				ObjectContext objectContext = ((IObjectContextAdapter)this.TransactionContext).ObjectContext;
				try
				{
					objectContext.SaveChangesInternal(SaveOptions.None, !useExecutionStrategy);
					this._rowsToDelete.Clear();
				}
				finally
				{
					objectContext.AcceptAllChanges();
				}
			}
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0005663C File Offset: 0x0005483C
		protected virtual async Task PruneTransactionHistoryAsync(bool force, bool useExecutionStrategy, CancellationToken cancellationToken)
		{
			if (this._rowsToDelete.Count > 0 && (force || this._rowsToDelete.Count > this.PruningLimit))
			{
				foreach (TransactionRow transactionRow in this.TransactionContext.Transactions.ToList<TransactionRow>())
				{
					if (this._rowsToDelete.Contains(transactionRow))
					{
						this.TransactionContext.Transactions.Remove(transactionRow);
					}
				}
				ObjectContext objectContext = ((IObjectContextAdapter)this.TransactionContext).ObjectContext;
				try
				{
					await ((IObjectContextAdapter)this.TransactionContext).ObjectContext.SaveChangesInternalAsync(SaveOptions.None, !useExecutionStrategy, cancellationToken).WithCurrentCulture<int>();
					this._rowsToDelete.Clear();
				}
				finally
				{
					objectContext.AcceptAllChanges();
				}
				objectContext = null;
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0005669C File Offset: 0x0005489C
		private void PruneTransactionHistory(TransactionRow transaction)
		{
			this.MarkTransactionForPruning(transaction);
			try
			{
				this.PruneTransactionHistory(false, false);
			}
			catch (DataException)
			{
			}
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x000566D0 File Offset: 0x000548D0
		public static CommitFailureHandler FromContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return CommitFailureHandler.FromContext(((IObjectContextAdapter)context).ObjectContext);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000566E9 File Offset: 0x000548E9
		public static CommitFailureHandler FromContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return context.TransactionHandler as CommitFailureHandler;
		}

		// Token: 0x04000B41 RID: 2881
		private readonly HashSet<TransactionRow> _rowsToDelete = new HashSet<TransactionRow>();

		// Token: 0x04000B42 RID: 2882
		private readonly Func<DbConnection, TransactionContext> _transactionContextFactory;
	}
}
