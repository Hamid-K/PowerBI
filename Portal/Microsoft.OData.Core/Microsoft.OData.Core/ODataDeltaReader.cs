using System;
using System.Threading.Tasks;

namespace Microsoft.OData
{
	// Token: 0x02000077 RID: 119
	public abstract class ODataDeltaReader
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000422 RID: 1058
		public abstract ODataDeltaReaderState State { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000423 RID: 1059
		public abstract ODataReaderState SubState { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000424 RID: 1060
		public abstract ODataItem Item { get; }

		// Token: 0x06000425 RID: 1061
		public abstract bool Read();

		// Token: 0x06000426 RID: 1062
		public abstract Task<bool> ReadAsync();
	}
}
