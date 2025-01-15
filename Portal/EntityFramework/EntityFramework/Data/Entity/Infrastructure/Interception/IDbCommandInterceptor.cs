using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200028D RID: 653
	public interface IDbCommandInterceptor : IDbInterceptor
	{
		// Token: 0x060020C5 RID: 8389
		void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext);

		// Token: 0x060020C6 RID: 8390
		void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext);

		// Token: 0x060020C7 RID: 8391
		void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext);

		// Token: 0x060020C8 RID: 8392
		void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext);

		// Token: 0x060020C9 RID: 8393
		void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext);

		// Token: 0x060020CA RID: 8394
		void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext);
	}
}
