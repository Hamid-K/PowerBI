using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Extensions
{
	// Token: 0x02000041 RID: 65
	public class UseHandlerMiddleware
	{
		// Token: 0x0600024D RID: 589 RVA: 0x000066D4 File Offset: 0x000048D4
		public UseHandlerMiddleware(Func<IDictionary<string, object>, Task> next, Func<IOwinContext, Task> handler)
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this._next = next;
			this._handler = handler;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000066F8 File Offset: 0x000048F8
		public UseHandlerMiddleware(Func<IDictionary<string, object>, Task> next, Func<IOwinContext, Func<Task>, Task> handler)
		{
			UseHandlerMiddleware <>4__this = this;
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this._next = next;
			this._handler = (IOwinContext context) => handler(context, () => <>4__this._next(context.Environment));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000674C File Offset: 0x0000494C
		public Task Invoke(IDictionary<string, object> environment)
		{
			IOwinContext context = new OwinContext(environment);
			return this._handler(context);
		}

		// Token: 0x04000073 RID: 115
		private readonly Func<IDictionary<string, object>, Task> _next;

		// Token: 0x04000074 RID: 116
		private readonly Func<IOwinContext, Task> _handler;
	}
}
