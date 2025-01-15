using System;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200028E RID: 654
	public interface IDbCommandTreeInterceptor : IDbInterceptor
	{
		// Token: 0x060020CB RID: 8395
		void TreeCreated(DbCommandTreeInterceptionContext interceptionContext);
	}
}
