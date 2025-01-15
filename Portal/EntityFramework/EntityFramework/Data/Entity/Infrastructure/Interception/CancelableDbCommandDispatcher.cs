using System;
using System.Data.Common;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000274 RID: 628
	internal class CancelableDbCommandDispatcher
	{
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x0005A0EF File Offset: 0x000582EF
		public InternalDispatcher<ICancelableDbCommandInterceptor> InternalDispatcher
		{
			get
			{
				return this._internalDispatcher;
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0005A0F8 File Offset: 0x000582F8
		public virtual bool Executing(DbCommand command, DbInterceptionContext interceptionContext)
		{
			return this._internalDispatcher.Dispatch<bool>(true, (bool b, ICancelableDbCommandInterceptor i) => i.CommandExecuting(command, interceptionContext) && b);
		}

		// Token: 0x04000B6C RID: 2924
		private readonly InternalDispatcher<ICancelableDbCommandInterceptor> _internalDispatcher = new InternalDispatcher<ICancelableDbCommandInterceptor>();
	}
}
