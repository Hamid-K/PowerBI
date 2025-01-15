using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000294 RID: 660
	public interface IDbTransactionInterceptor : IDbInterceptor
	{
		// Token: 0x060020E7 RID: 8423
		void ConnectionGetting(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext);

		// Token: 0x060020E8 RID: 8424
		void ConnectionGot(DbTransaction transaction, DbTransactionInterceptionContext<DbConnection> interceptionContext);

		// Token: 0x060020E9 RID: 8425
		void IsolationLevelGetting(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext);

		// Token: 0x060020EA RID: 8426
		void IsolationLevelGot(DbTransaction transaction, DbTransactionInterceptionContext<IsolationLevel> interceptionContext);

		// Token: 0x060020EB RID: 8427
		void Committing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x060020EC RID: 8428
		void Committed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x060020ED RID: 8429
		void Disposing(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x060020EE RID: 8430
		void Disposed(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x060020EF RID: 8431
		void RollingBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);

		// Token: 0x060020F0 RID: 8432
		void RolledBack(DbTransaction transaction, DbTransactionInterceptionContext interceptionContext);
	}
}
