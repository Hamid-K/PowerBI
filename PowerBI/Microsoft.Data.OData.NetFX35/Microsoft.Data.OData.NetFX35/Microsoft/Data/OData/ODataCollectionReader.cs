using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014B RID: 331
	public abstract class ODataCollectionReader
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060008C4 RID: 2244
		public abstract ODataCollectionReaderState State { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060008C5 RID: 2245
		public abstract object Item { get; }

		// Token: 0x060008C6 RID: 2246
		public abstract bool Read();
	}
}
