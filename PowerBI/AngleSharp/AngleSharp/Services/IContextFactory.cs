using System;
using AngleSharp.Dom;

namespace AngleSharp.Services
{
	// Token: 0x02000026 RID: 38
	public interface IContextFactory
	{
		// Token: 0x0600011C RID: 284
		IBrowsingContext Create(IConfiguration configuration, Sandboxes security);

		// Token: 0x0600011D RID: 285
		IBrowsingContext Create(IBrowsingContext parent, string name, Sandboxes security);

		// Token: 0x0600011E RID: 286
		IBrowsingContext Find(string name);
	}
}
