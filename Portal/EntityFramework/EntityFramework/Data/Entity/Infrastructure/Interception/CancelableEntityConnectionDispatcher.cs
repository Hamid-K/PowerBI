using System;
using System.Data.Entity.Core.EntityClient;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000275 RID: 629
	internal class CancelableEntityConnectionDispatcher
	{
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001F9B RID: 8091 RVA: 0x0005A144 File Offset: 0x00058344
		public InternalDispatcher<ICancelableEntityConnectionInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x0005A14C File Offset: 0x0005834C
		public virtual bool Opening(EntityConnection entityConnection, DbInterceptionContext interceptionContext)
		{
			return this._internalDispatcher.Dispatch<bool>(true, (bool b, ICancelableEntityConnectionInterceptor i) => i.ConnectionOpening(entityConnection, interceptionContext) && b);
		}

		// Token: 0x04000B6D RID: 2925
		private readonly InternalDispatcher<ICancelableEntityConnectionInterceptor> _internalDispatcher = new InternalDispatcher<ICancelableEntityConnectionInterceptor>();
	}
}
