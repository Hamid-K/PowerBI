using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200027C RID: 636
	internal class DbCommandTreeDispatcher
	{
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x0005B406 File Offset: 0x00059606
		public InternalDispatcher<IDbCommandTreeInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0005B40E File Offset: 0x0005960E
		public virtual DbCommandTree Created(DbCommandTree commandTree, DbInterceptionContext interceptionContext)
		{
			return this._internalDispatcher.Dispatch<DbCommandTreeInterceptionContext, DbCommandTree>(commandTree, new DbCommandTreeInterceptionContext(interceptionContext), delegate(IDbCommandTreeInterceptor i, DbCommandTreeInterceptionContext c)
			{
				i.TreeCreated(c);
			});
		}

		// Token: 0x04000B78 RID: 2936
		private readonly InternalDispatcher<IDbCommandTreeInterceptor> _internalDispatcher = new InternalDispatcher<IDbCommandTreeInterceptor>();
	}
}
