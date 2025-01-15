using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x02000030 RID: 48
	internal sealed class AppFuncTransition : OwinMiddleware
	{
		// Token: 0x060001EE RID: 494 RVA: 0x00004F87 File Offset: 0x00003187
		public AppFuncTransition(Func<IDictionary<string, object>, Task> next)
			: base(null)
		{
			this._next = next;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00004F97 File Offset: 0x00003197
		public override Task Invoke(IOwinContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}
			return this._next(context.Environment);
		}

		// Token: 0x04000061 RID: 97
		private readonly Func<IDictionary<string, object>, Task> _next;
	}
}
