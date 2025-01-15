using System;
using System.Data.Entity.Core.EntityClient;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200028C RID: 652
	internal interface ICancelableEntityConnectionInterceptor : IDbInterceptor
	{
		// Token: 0x060020C4 RID: 8388
		bool ConnectionOpening(EntityConnection connection, DbInterceptionContext interceptionContext);
	}
}
