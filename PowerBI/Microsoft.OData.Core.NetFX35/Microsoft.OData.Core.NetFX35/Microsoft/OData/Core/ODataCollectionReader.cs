using System;

namespace Microsoft.OData.Core
{
	// Token: 0x0200002E RID: 46
	public abstract class ODataCollectionReader
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001AA RID: 426
		public abstract ODataCollectionReaderState State { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001AB RID: 427
		public abstract object Item { get; }

		// Token: 0x060001AC RID: 428
		public abstract bool Read();
	}
}
