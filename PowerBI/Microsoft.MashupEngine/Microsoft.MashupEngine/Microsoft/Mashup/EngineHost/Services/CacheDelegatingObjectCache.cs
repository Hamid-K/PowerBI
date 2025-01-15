using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x020019AF RID: 6575
	public abstract class CacheDelegatingObjectCache : DelegatingObjectCache
	{
		// Token: 0x0600A6A0 RID: 42656 RVA: 0x0022768F File Offset: 0x0022588F
		protected CacheDelegatingObjectCache(IObjectCache cache)
		{
			this.cache = cache;
		}

		// Token: 0x17002A84 RID: 10884
		// (get) Token: 0x0600A6A1 RID: 42657 RVA: 0x0022769E File Offset: 0x0022589E
		protected sealed override IObjectCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x040056B1 RID: 22193
		private readonly IObjectCache cache;
	}
}
