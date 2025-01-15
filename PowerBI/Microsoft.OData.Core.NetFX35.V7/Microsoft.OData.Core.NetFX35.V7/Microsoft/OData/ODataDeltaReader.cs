using System;

namespace Microsoft.OData
{
	// Token: 0x02000054 RID: 84
	public abstract class ODataDeltaReader
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002A4 RID: 676
		public abstract ODataDeltaReaderState State { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002A5 RID: 677
		public abstract ODataReaderState SubState { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002A6 RID: 678
		public abstract ODataItem Item { get; }

		// Token: 0x060002A7 RID: 679
		public abstract bool Read();
	}
}
