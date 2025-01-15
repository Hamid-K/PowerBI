using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000236 RID: 566
	internal interface IStoreSchema
	{
		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060012C3 RID: 4803
		int RootBitMaskSize { get; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060012C4 RID: 4804
		int SubDirectoryBitMaskSize { get; }
	}
}
