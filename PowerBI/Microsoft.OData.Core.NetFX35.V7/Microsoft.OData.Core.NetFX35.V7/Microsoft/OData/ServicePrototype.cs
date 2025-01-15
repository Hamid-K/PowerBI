using System;

namespace Microsoft.OData
{
	// Token: 0x020000CD RID: 205
	internal class ServicePrototype<TService>
	{
		// Token: 0x060007D2 RID: 2002 RVA: 0x00015CDF File Offset: 0x00013EDF
		public ServicePrototype(TService instance)
		{
			this.Instance = instance;
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00015CEE File Offset: 0x00013EEE
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00015CF6 File Offset: 0x00013EF6
		public TService Instance { get; private set; }
	}
}
