using System;
using System.Collections.Generic;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x0200010E RID: 270
	internal interface IDomainLayoutCollectionConfiguration
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000733 RID: 1843
		IEnumerable<IDomainLayoutConfiguration> DomainLayout { get; }
	}
}
