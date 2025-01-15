using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200028B RID: 651
	internal interface ICancelableDbCommandInterceptor : IDbInterceptor
	{
		// Token: 0x060020C3 RID: 8387
		bool CommandExecuting(DbCommand command, DbInterceptionContext interceptionContext);
	}
}
