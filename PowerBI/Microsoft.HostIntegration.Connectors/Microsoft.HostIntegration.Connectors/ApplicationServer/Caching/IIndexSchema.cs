using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000217 RID: 535
	internal interface IIndexSchema
	{
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060011C9 RID: 4553
		// (set) Token: 0x060011CA RID: 4554
		GetKeyFromCacheItemDelegate TagExtractorDelegate { get; set; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060011CB RID: 4555
		// (set) Token: 0x060011CC RID: 4556
		IIndexStoreSchema IndexStoreSchema { get; set; }
	}
}
