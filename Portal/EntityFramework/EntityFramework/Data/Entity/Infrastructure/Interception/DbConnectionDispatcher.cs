using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000280 RID: 640
	public class DbConnectionDispatcher
	{
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x0005B663 File Offset: 0x00059863
		internal InternalDispatcher<IDbConnectionInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0005B66B File Offset: 0x0005986B
		internal DbConnectionDispatcher()
		{
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0005B680 File Offset: 0x00059880
		public virtual DbTransaction BeginTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<BeginTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, BeginTransactionInterceptionContext, DbTransaction>(connection, (DbConnection t, BeginTransactionInterceptionContext c) => t.BeginTransaction(c.IsolationLevel), new BeginTransactionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, BeginTransactionInterceptionContext c)
			{
				i.BeginningTransaction(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, BeginTransactionInterceptionContext c)
			{
				i.BeganTransaction(t, c);
			});
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0005B714 File Offset: 0x00059914
		public virtual void Close(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext>(connection, delegate(DbConnection t, DbConnectionInterceptionContext c)
			{
				t.Close();
			}, new DbConnectionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Closing(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Closed(t, c);
			});
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0005B7A8 File Offset: 0x000599A8
		public virtual void Dispose(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext>(connection, delegate(DbConnection t, DbConnectionInterceptionContext c)
			{
				try
				{
				}
				finally
				{
					if (t != null)
					{
						((IDisposable)t).Dispose();
					}
				}
			}, new DbConnectionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Disposing(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Disposed(t, c);
			});
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0005B83C File Offset: 0x00059A3C
		public virtual string GetConnectionString(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.ConnectionString, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ConnectionStringGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ConnectionStringGot(t, c);
			});
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0005B8D0 File Offset: 0x00059AD0
		public virtual void SetConnectionString(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbConnectionPropertyInterceptionContext<string>>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionPropertyInterceptionContext<string>>(connection, delegate(DbConnection t, DbConnectionPropertyInterceptionContext<string> c)
			{
				t.ConnectionString = c.Value;
			}, new DbConnectionPropertyInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionPropertyInterceptionContext<string> c)
			{
				i.ConnectionStringSetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionPropertyInterceptionContext<string> c)
			{
				i.ConnectionStringSet(t, c);
			});
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0005B964 File Offset: 0x00059B64
		public virtual int GetConnectionTimeout(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<int>, int>(connection, (DbConnection t, DbConnectionInterceptionContext<int> c) => t.ConnectionTimeout, new DbConnectionInterceptionContext<int>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<int> c)
			{
				i.ConnectionTimeoutGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<int> c)
			{
				i.ConnectionTimeoutGot(t, c);
			});
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0005B9F8 File Offset: 0x00059BF8
		public virtual string GetDatabase(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.Database, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DatabaseGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DatabaseGot(t, c);
			});
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0005BA8C File Offset: 0x00059C8C
		public virtual string GetDataSource(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.DataSource, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DataSourceGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.DataSourceGot(t, c);
			});
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0005BB20 File Offset: 0x00059D20
		public virtual void EnlistTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<EnlistTransactionInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, EnlistTransactionInterceptionContext>(connection, delegate(DbConnection t, EnlistTransactionInterceptionContext c)
			{
				t.EnlistTransaction(c.Transaction);
			}, new EnlistTransactionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, EnlistTransactionInterceptionContext c)
			{
				i.EnlistingTransaction(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, EnlistTransactionInterceptionContext c)
			{
				i.EnlistedTransaction(t, c);
			});
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		public virtual void Open(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext>(connection, delegate(DbConnection t, DbConnectionInterceptionContext c)
			{
				t.Open();
			}, new DbConnectionInterceptionContext(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opening(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opened(t, c);
			});
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0005BC48 File Offset: 0x00059E48
		public virtual Task OpenAsync(DbConnection connection, DbInterceptionContext interceptionContext, CancellationToken cancellationToken)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.DispatchAsync<DbConnection, DbConnectionInterceptionContext>(connection, (DbConnection t, DbConnectionInterceptionContext c, CancellationToken ct) => t.OpenAsync(ct), new DbConnectionInterceptionContext(interceptionContext).AsAsync(), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opening(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext c)
			{
				i.Opened(t, c);
			}, cancellationToken);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0005BCE4 File Offset: 0x00059EE4
		public virtual string GetServerVersion(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<string>, string>(connection, (DbConnection t, DbConnectionInterceptionContext<string> c) => t.ServerVersion, new DbConnectionInterceptionContext<string>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ServerVersionGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<string> c)
			{
				i.ServerVersionGot(t, c);
			});
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0005BD78 File Offset: 0x00059F78
		public virtual ConnectionState GetState(DbConnection connection, DbInterceptionContext interceptionContext)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			Check.NotNull<DbInterceptionContext>(interceptionContext, "interceptionContext");
			return this.InternalDispatcher.Dispatch<DbConnection, DbConnectionInterceptionContext<ConnectionState>, ConnectionState>(connection, (DbConnection t, DbConnectionInterceptionContext<ConnectionState> c) => t.State, new DbConnectionInterceptionContext<ConnectionState>(interceptionContext), delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<ConnectionState> c)
			{
				i.StateGetting(t, c);
			}, delegate(IDbConnectionInterceptor i, DbConnection t, DbConnectionInterceptionContext<ConnectionState> c)
			{
				i.StateGot(t, c);
			});
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0005BE0C File Offset: 0x0005A00C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x0005BE14 File Offset: 0x0005A014
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x0005BE1D File Offset: 0x0005A01D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0005BE25 File Offset: 0x0005A025
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B7B RID: 2939
		private readonly InternalDispatcher<IDbConnectionInterceptor> _internalDispatcher = new InternalDispatcher<IDbConnectionInterceptor>();
	}
}
