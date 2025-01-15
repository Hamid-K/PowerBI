using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200027B RID: 635
	public class DbCommandInterceptor : IDbCommandInterceptor, IDbInterceptor
	{
		// Token: 0x06002016 RID: 8214 RVA: 0x0005B3F2 File Offset: 0x000595F2
		public virtual void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x0005B3F4 File Offset: 0x000595F4
		public virtual void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
		{
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x0005B3F6 File Offset: 0x000595F6
		public virtual void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0005B3F8 File Offset: 0x000595F8
		public virtual void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
		{
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0005B3FA File Offset: 0x000595FA
		public virtual void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0005B3FC File Offset: 0x000595FC
		public virtual void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
		{
		}
	}
}
