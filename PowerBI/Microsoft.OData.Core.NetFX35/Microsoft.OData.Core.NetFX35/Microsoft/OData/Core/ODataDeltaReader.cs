using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020000C3 RID: 195
	public abstract class ODataDeltaReader
	{
		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600070A RID: 1802
		public abstract ODataDeltaReaderState State { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600070B RID: 1803
		public abstract ODataReaderState SubState { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600070C RID: 1804
		public abstract ODataItem Item { get; }

		// Token: 0x0600070D RID: 1805
		public abstract bool Read();
	}
}
