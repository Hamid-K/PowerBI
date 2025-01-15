using System;
using System.Collections.Generic;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x0200000F RID: 15
	public sealed class ExecutionMetricsCachedResultSet
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002A88 File Offset: 0x00000C88
		public ExecutionMetricsCachedResultSet(IReadOnlyList<ExecutionEventData> events, bool wasTruncated)
		{
			this.Events = events;
			this.WasTruncated = wasTruncated;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002A9E File Offset: 0x00000C9E
		public IReadOnlyList<ExecutionEventData> Events { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002AA6 File Offset: 0x00000CA6
		public bool WasTruncated { get; }
	}
}
