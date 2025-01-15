using System;

namespace Microsoft.OData
{
	// Token: 0x0200003E RID: 62
	public abstract class ODataCollectionReader
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001FD RID: 509
		public abstract ODataCollectionReaderState State { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001FE RID: 510
		public abstract object Item { get; }

		// Token: 0x060001FF RID: 511
		public abstract bool Read();
	}
}
