using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x02000044 RID: 68
	internal sealed class InstanceAnnotationWriteTracker
	{
		// Token: 0x06000232 RID: 562 RVA: 0x0000619C File Offset: 0x0000439C
		public InstanceAnnotationWriteTracker()
		{
			this.writeStatus = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000061B4 File Offset: 0x000043B4
		public bool IsAnnotationWritten(string key)
		{
			return this.writeStatus.Contains(key);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000061C2 File Offset: 0x000043C2
		public bool MarkAnnotationWritten(string key)
		{
			return this.writeStatus.Add(key);
		}

		// Token: 0x040000A0 RID: 160
		private readonly HashSet<string> writeStatus;
	}
}
