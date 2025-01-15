using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200010D RID: 269
	internal interface IDomainLayoutConfiguration
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000730 RID: 1840
		string DomainName { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000731 RID: 1841
		Uri DomainAddress { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000732 RID: 1842
		string DomainCategory { get; }
	}
}
