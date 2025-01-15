using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000118 RID: 280
	internal interface IHostNodeDomainConfiguration
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007B3 RID: 1971
		// (set) Token: 0x060007B4 RID: 1972
		string Name { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007B5 RID: 1973
		Uri DomainAddress { get; }
	}
}
