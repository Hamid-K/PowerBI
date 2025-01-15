using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000266 RID: 614
	public abstract class TransactionHandler : IDbTransactionInterceptor, IDbInterceptor, IDbConnectionInterceptor, IDisposable
	{
		// Token: 0x06001F1B RID: 7963 RVA: 0x0005697E File Offset: 0x00054B7E
		protected TransactionHandler()
		{
			DbInterception.Add(this);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0005698C File Offset: 0x00054B8C
		public virtual void Initialize(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			if (this.ObjectContext != null || this.DbContext != null || this.Connection != null)
			{
				throw new InvalidOperationException(Strings.TransactionHandler_AlreadyInitialized);
			}
			this.ObjectContext = context;
			this.DbContext = context.InterceptionContext.DbContexts.FirstOrDefault<DbContext>();
			this.Connection = ((EntityConnection)this.ObjectContext.Connection).StoreConnection;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00056A00 File Offset: 0x00054C00
		public virtual void Initialize(DbContext context, DbConnection connection)
		{
			Check.NotNull<DbContext>(context, "context");
			Check.NotNull<DbConnection>(connection, "connection");
			if (this.ObjectContext != null || this.DbContext != null || this.Connection != null)
			{
				throw new InvalidOperationException(Strings.TransactionHandler_AlreadyInitialized);
			}
			this.DbContext = context;
			this.Connection = connection;
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x00056A56 File Offset: 0x00054C56
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x00056A7F File Offset: 0x00054C7F
		public ObjectContext ObjectContext
		{
			get
			{
				if (this._objectContext == null || !this._objectContext.IsAlive)
				{
					return null;
				}
				return (ObjectContext)this._objectContext.Target;
			}
			private set
			{
				this._objectContext = new WeakReference(value);
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x00056A8D File Offset: 0x00054C8D
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x00056AB6 File Offset: 0x00054CB6
		public DbContext DbContext
		{
			get
			{
				if (this._dbContext == null || !this._dbContext.IsAlive)
				{
					return null;
				}
				return (DbContext)this._dbContext.Target;
			}
			private set
			{
				this._dbContext = new WeakReference(value);
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x00056AC4 File Offset: 0x00054CC4
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x00056AED File Offset: 0x00054CED
		public DbConnection Connection
		{
			get
			{
				if (this._connection == null || !this._connection.IsAlive)
				{
					return null;
				}
				return (DbConnection)this._connection.Target;
			}
			private set
			{
				this._connection = new WeakReference(value);
			}
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00056AFB File Offset: 0x00054CFB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001F25 RID: 7973 RVA: 0x00056B0A File Offset: 0x00054D0A
		// (set) Token: 0x06001F26 RID: 7974 RVA: 0x00056B12 File Offset: 0x00054D12
		protected bool IsDisposed { get; set; }

		// Token: 0x06001F27 RID: 7975 RVA: 0x00056B1B File Offset: 0x00054D1B
		protected virtual void Dispose(bool disposing)
		{
			if (!this.IsDisposed)
			{
				DbInterception.Remove(this);
				this.IsDisposed = true;
			}
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00056B34 File Offset: 0x00054D34
		protected internal virtual bool MatchesParentContext(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return (this.DbContext != null && interceptionContext.DbContexts.Contains(this.DbContext, new Func<DbContext, DbContext, bool>(object.ReferenceEquals))) || (this.ObjectContext != null && interceptionContext.ObjectContexts.Contains(this.ObjectContext, new Func<ObjectContext, ObjectContext, bool>(object.ReferenceEquals))) || (this.Connection != null && !interceptionContext.ObjectContexts.Any<ObjectContext>() && !interceptionContext.DbContexts.Any<DbContext>() && connection == this.Connection);
		}

		// Token: 0x06001F29 RID: 7977
		public abstract string BuildDatabaseInitializationScript();

		// Token: 0x06001F2A RID: 7978 RVA: 0x00056BD8 File Offset: 0x00054DD8
		public virtual void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00056BDA File Offset: 0x00054DDA
		public virtual void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00056BDC File Offset: 0x00054DDC
		public virtual void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00056BDE File Offset: 0x00054DDE
		public virtual void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00056BE0 File Offset: 0x00054DE0
		public virtual void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00056BE2 File Offset: 0x00054DE2
		public virtual void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00056BE4 File Offset: 0x00054DE4
		public virtual void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00056BE6 File Offset: 0x00054DE6
		public virtual void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00056BE8 File Offset: 0x00054DE8
		public virtual void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00056BEA File Offset: 0x00054DEA
		public virtual void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00056BEC File Offset: 0x00054DEC
		public virtual void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00056BEE File Offset: 0x00054DEE
		public virtual void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00056BF0 File Offset: 0x00054DF0
		public virtual void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00056BF2 File Offset: 0x00054DF2
		public virtual void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00056BF4 File Offset: 0x00054DF4
		public virtual void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00056BF6 File Offset: 0x00054DF6
		public virtual void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00056BF8 File Offset: 0x00054DF8
		public virtual void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00056BFA File Offset: 0x00054DFA
		public virtual void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00056BFC File Offset: 0x00054DFC
		public virtual void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00056BFE File Offset: 0x00054DFE
		public virtual void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00056C00 File Offset: 0x00054E00
		public virtual void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00056C02 File Offset: 0x00054E02
		public virtual void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext)
		{
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00056C04 File Offset: 0x00054E04
		public virtual void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00056C06 File Offset: 0x00054E06
		public virtual void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext)
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00056C08 File Offset: 0x00054E08
		public virtual void ConnectionGetting(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00056C0A File Offset: 0x00054E0A
		public virtual void ConnectionGot(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext)
		{
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00056C0C File Offset: 0x00054E0C
		public virtual void IsolationLevelGetting(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00056C0E File Offset: 0x00054E0E
		public virtual void IsolationLevelGot(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext)
		{
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00056C10 File Offset: 0x00054E10
		public virtual void Committing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F47 RID: 8007 RVA: 0x00056C12 File Offset: 0x00054E12
		public virtual void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F48 RID: 8008 RVA: 0x00056C14 File Offset: 0x00054E14
		public virtual void Disposing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F49 RID: 8009 RVA: 0x00056C16 File Offset: 0x00054E16
		public virtual void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F4A RID: 8010 RVA: 0x00056C18 File Offset: 0x00054E18
		public virtual void RollingBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x06001F4B RID: 8011 RVA: 0x00056C1A File Offset: 0x00054E1A
		public virtual void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext)
		{
		}

		// Token: 0x04000B47 RID: 2887
		private WeakReference _objectContext;

		// Token: 0x04000B48 RID: 2888
		private WeakReference _dbContext;

		// Token: 0x04000B49 RID: 2889
		private WeakReference _connection;
	}
}
