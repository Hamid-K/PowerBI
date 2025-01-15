using System;

namespace Microsoft.OData
{
	// Token: 0x02000091 RID: 145
	public abstract class ODataReader
	{
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000595 RID: 1429
		public abstract ODataReaderState State { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000596 RID: 1430
		public abstract ODataItem Item { get; }

		// Token: 0x06000597 RID: 1431
		public abstract bool Read();
	}
}
