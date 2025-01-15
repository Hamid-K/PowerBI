using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000091 RID: 145
	internal interface IMemberCollectionInternal
	{
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008A4 RID: 2212
		int Count { get; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008A5 RID: 2213
		bool IsSynchronized { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008A6 RID: 2214
		object SyncRoot { get; }

		// Token: 0x1700029A RID: 666
		Member this[int index] { get; }

		// Token: 0x060008A8 RID: 2216
		Member Find(string index);
	}
}
