using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004D3 RID: 1235
	internal interface IIndexedInCollection
	{
		// Token: 0x17001A8E RID: 6798
		// (get) Token: 0x06003E83 RID: 16003
		// (set) Token: 0x06003E84 RID: 16004
		int IndexInCollection { get; set; }

		// Token: 0x17001A8F RID: 6799
		// (get) Token: 0x06003E85 RID: 16005
		IndexedInCollectionType IndexedInCollectionType { get; }
	}
}
