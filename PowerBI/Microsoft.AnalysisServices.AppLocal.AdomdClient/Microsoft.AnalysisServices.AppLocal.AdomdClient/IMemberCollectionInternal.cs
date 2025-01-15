using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000091 RID: 145
	internal interface IMemberCollectionInternal
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060008B1 RID: 2225
		int Count { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060008B2 RID: 2226
		bool IsSynchronized { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060008B3 RID: 2227
		object SyncRoot { get; }

		// Token: 0x170002A0 RID: 672
		Member this[int index] { get; }

		// Token: 0x060008B5 RID: 2229
		Member Find(string index);
	}
}
