using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200016A RID: 362
	[Flags]
	public enum DataViewUnsupportedReasons
	{
		// Token: 0x04000535 RID: 1333
		None = 0,
		// Token: 0x04000536 RID: 1334
		DirectQuery = 1,
		// Token: 0x04000537 RID: 1335
		AggregationsTableWithRLSEmulationMode = 2
	}
}
