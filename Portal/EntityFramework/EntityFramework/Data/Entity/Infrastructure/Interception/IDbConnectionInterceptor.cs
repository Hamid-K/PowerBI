using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000290 RID: 656
	public interface IDbConnectionInterceptor : IDbInterceptor
	{
		// Token: 0x060020CD RID: 8397
		void BeginningTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext);

		// Token: 0x060020CE RID: 8398
		void BeganTransaction(DbConnection connection, BeginTransactionInterceptionContext interceptionContext);

		// Token: 0x060020CF RID: 8399
		void Closing(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x060020D0 RID: 8400
		void Closed(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x060020D1 RID: 8401
		void ConnectionStringGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020D2 RID: 8402
		void ConnectionStringGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020D3 RID: 8403
		void ConnectionStringSetting(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext);

		// Token: 0x060020D4 RID: 8404
		void ConnectionStringSet(DbConnection connection, DbConnectionPropertyInterceptionContext<string> interceptionContext);

		// Token: 0x060020D5 RID: 8405
		void ConnectionTimeoutGetting(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext);

		// Token: 0x060020D6 RID: 8406
		void ConnectionTimeoutGot(DbConnection connection, DbConnectionInterceptionContext<int> interceptionContext);

		// Token: 0x060020D7 RID: 8407
		void DatabaseGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020D8 RID: 8408
		void DatabaseGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020D9 RID: 8409
		void DataSourceGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020DA RID: 8410
		void DataSourceGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020DB RID: 8411
		void Disposing(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x060020DC RID: 8412
		void Disposed(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x060020DD RID: 8413
		void EnlistingTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext);

		// Token: 0x060020DE RID: 8414
		void EnlistedTransaction(DbConnection connection, EnlistTransactionInterceptionContext interceptionContext);

		// Token: 0x060020DF RID: 8415
		void Opening(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x060020E0 RID: 8416
		void Opened(DbConnection connection, DbConnectionInterceptionContext interceptionContext);

		// Token: 0x060020E1 RID: 8417
		void ServerVersionGetting(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020E2 RID: 8418
		void ServerVersionGot(DbConnection connection, DbConnectionInterceptionContext<string> interceptionContext);

		// Token: 0x060020E3 RID: 8419
		void StateGetting(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext);

		// Token: 0x060020E4 RID: 8420
		void StateGot(DbConnection connection, DbConnectionInterceptionContext<ConnectionState> interceptionContext);
	}
}
