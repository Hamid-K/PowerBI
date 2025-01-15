using System;
using System.Collections.Generic;

namespace Microsoft.OData.Core
{
	// Token: 0x020000A6 RID: 166
	internal sealed class InstanceAnnotationWriteTracker
	{
		// Token: 0x06000623 RID: 1571 RVA: 0x00016060 File Offset: 0x00014260
		public InstanceAnnotationWriteTracker()
		{
			this.writeStatus = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00016078 File Offset: 0x00014278
		public bool IsAnnotationWritten(string key)
		{
			return this.writeStatus.Contains(key);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00016086 File Offset: 0x00014286
		public bool MarkAnnotationWritten(string key)
		{
			return this.writeStatus.Add(key);
		}

		// Token: 0x04000287 RID: 647
		private readonly HashSet<string> writeStatus;
	}
}
