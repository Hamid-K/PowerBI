using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x0200001A RID: 26
	internal class TupleInternal<T1, T2>
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00007547 File Offset: 0x00005747
		public TupleInternal(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000755D File Offset: 0x0000575D
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00007565 File Offset: 0x00005765
		public T1 Item1 { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000756E File Offset: 0x0000576E
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00007576 File Offset: 0x00005776
		public T2 Item2 { get; private set; }
	}
}
