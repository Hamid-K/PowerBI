using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000117 RID: 279
	internal interface IClientSideHostConfiguration
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060007AE RID: 1966
		// (set) Token: 0x060007AF RID: 1967
		string Name { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060007B0 RID: 1968
		// (set) Token: 0x060007B1 RID: 1969
		int ServicePort { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007B2 RID: 1970
		string ServiceURI { get; }
	}
}
