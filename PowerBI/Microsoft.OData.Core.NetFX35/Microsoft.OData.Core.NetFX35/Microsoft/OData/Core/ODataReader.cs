using System;

namespace Microsoft.OData.Core
{
	// Token: 0x02000054 RID: 84
	public abstract class ODataReader
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600034E RID: 846
		public abstract ODataReaderState State { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600034F RID: 847
		public abstract ODataItem Item { get; }

		// Token: 0x06000350 RID: 848
		public abstract bool Read();
	}
}
