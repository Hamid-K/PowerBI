using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001EC RID: 492
	internal interface IEvictionCandidate
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000FFA RID: 4090
		long LastAccess { get; }

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000FFB RID: 4091
		object GetItemToEvict { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000FFC RID: 4092
		bool ImmediateCleanup { get; }
	}
}
