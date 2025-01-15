using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.Owin.Infrastructure
{
	// Token: 0x0200003A RID: 58
	internal sealed class OwinMiddlewareTransition
	{
		// Token: 0x0600023C RID: 572 RVA: 0x000063C5 File Offset: 0x000045C5
		public OwinMiddlewareTransition(OwinMiddleware next)
		{
			this._next = next;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000063D4 File Offset: 0x000045D4
		public Task Invoke(IDictionary<string, object> environment)
		{
			return this._next.Invoke(new OwinContext(environment));
		}

		// Token: 0x04000070 RID: 112
		private readonly OwinMiddleware _next;
	}
}
