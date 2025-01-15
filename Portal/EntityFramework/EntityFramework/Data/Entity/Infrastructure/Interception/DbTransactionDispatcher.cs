using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000287 RID: 647
	public class DbTransactionDispatcher
	{
		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x0005C474 File Offset: 0x0005A674
		internal InternalDispatcher<IDbTransactionInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x0005C47C File Offset: 0x0005A67C
		internal DbTransactionDispatcher()
		{
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0005C490 File Offset: 0x0005A690
		public virtual DbConnection GetConnection(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext<DbConnection>, DbConnection>(transaction, (DbTransaction t, DbTransactionInterceptionContext<DbConnection> c) => t.Connection, new DbTransactionInterceptionContext<DbConnection>(interceptionContext), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<DbConnection> c)
			{
				i.ConnectionGetting(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<DbConnection> c)
			{
				i.ConnectionGot(t, c);
			});
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0005C524 File Offset: 0x0005A724
		public virtual IsolationLevel GetIsolationLevel(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext<IsolationLevel>, IsolationLevel>(transaction, (DbTransaction t, DbTransactionInterceptionContext<IsolationLevel> c) => t.IsolationLevel, new DbTransactionInterceptionContext<IsolationLevel>(interceptionContext), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<IsolationLevel> c)
			{
				i.IsolationLevelGetting(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext<IsolationLevel> c)
			{
				i.IsolationLevelGot(t, c);
			});
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x0005C5B8 File Offset: 0x0005A7B8
		public virtual void Commit(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext>(transaction, delegate(DbTransaction t, DbTransactionInterceptionContext c)
			{
				t.Commit();
			}, new DbTransactionInterceptionContext(interceptionContext).WithConnection(transaction.Connection), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Committing(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Committed(t, c);
			});
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x0005C658 File Offset: 0x0005A858
		public virtual void Dispose(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			DbTransactionInterceptionContext dbTransactionInterceptionContext = new DbTransactionInterceptionContext(interceptionContext);
			if (transaction.Connection != null)
			{
				dbTransactionInterceptionContext = dbTransactionInterceptionContext.WithConnection(transaction.Connection);
			}
			this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext>(transaction, delegate(DbTransaction t, DbTransactionInterceptionContext c)
			{
				t.Dispose();
			}, dbTransactionInterceptionContext, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Disposing(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.Disposed(t, c);
			});
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x0005C704 File Offset: 0x0005A904
		public virtual void Rollback(DbTransaction transaction, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbTransaction>(transaction, "transaction");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbTransaction, DbTransactionInterceptionContext>(transaction, delegate(DbTransaction t, DbTransactionInterceptionContext c)
			{
				t.Rollback();
			}, new DbTransactionInterceptionContext(interceptionContext).WithConnection(transaction.Connection), delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.RollingBack(t, c);
			}, delegate(IDbTransactionInterceptor i, DbTransaction t, DbTransactionInterceptionContext c)
			{
				i.RolledBack(t, c);
			});
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0005C7A3 File Offset: 0x0005A9A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0005C7AB File Offset: 0x0005A9AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0005C7B4 File Offset: 0x0005A9B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0005C7BC File Offset: 0x0005A9BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B87 RID: 2951
		private readonly InternalDispatcher<IDbTransactionInterceptor> _internalDispatcher = new InternalDispatcher<IDbTransactionInterceptor>();
	}
}
