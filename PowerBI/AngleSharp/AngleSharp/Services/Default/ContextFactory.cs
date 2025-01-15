using System;
using System.Collections.Generic;
using AngleSharp.Dom;

namespace AngleSharp.Services.Default
{
	// Token: 0x02000046 RID: 70
	public class ContextFactory : IContextFactory
	{
		// Token: 0x06000167 RID: 359 RVA: 0x000074AB File Offset: 0x000056AB
		public IBrowsingContext Create(IConfiguration configuration, Sandboxes security)
		{
			return new BrowsingContext(configuration, security);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000074B4 File Offset: 0x000056B4
		public IBrowsingContext Create(IBrowsingContext parent, string name, Sandboxes security)
		{
			BrowsingContext browsingContext = new BrowsingContext(parent, security);
			this._cache[name] = new WeakReference<IBrowsingContext>(browsingContext);
			return browsingContext;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000074DC File Offset: 0x000056DC
		public IBrowsingContext Find(string name)
		{
			WeakReference<IBrowsingContext> weakReference = null;
			IBrowsingContext browsingContext = null;
			if (this._cache.TryGetValue(name, out weakReference))
			{
				weakReference.TryGetTarget(out browsingContext);
			}
			return browsingContext;
		}

		// Token: 0x040001C0 RID: 448
		private readonly Dictionary<string, WeakReference<IBrowsingContext>> _cache = new Dictionary<string, WeakReference<IBrowsingContext>>();
	}
}
