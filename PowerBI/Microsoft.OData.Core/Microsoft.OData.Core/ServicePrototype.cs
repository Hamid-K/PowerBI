using System;

namespace Microsoft.OData
{
	// Token: 0x020000CD RID: 205
	internal class ServicePrototype<TService>
	{
		// Token: 0x0600098A RID: 2442 RVA: 0x00018297 File Offset: 0x00016497
		public ServicePrototype(TService instance)
		{
			this.Instance = instance;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x000182A6 File Offset: 0x000164A6
		// (set) Token: 0x0600098C RID: 2444 RVA: 0x000182AE File Offset: 0x000164AE
		public TService Instance { get; private set; }
	}
}
