using System;
using System.Collections.Generic;

namespace Microsoft.Data.OData
{
	// Token: 0x0200013C RID: 316
	internal sealed class InstanceAnnotationWriteTracker
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x0001AFEC File Offset: 0x000191EC
		public InstanceAnnotationWriteTracker()
		{
			this.writeStatus = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B004 File Offset: 0x00019204
		public bool IsAnnotationWritten(string key)
		{
			return this.writeStatus.Contains(key);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B012 File Offset: 0x00019212
		public bool MarkAnnotationWritten(string key)
		{
			return this.writeStatus.Add(key);
		}

		// Token: 0x04000333 RID: 819
		private readonly HashSet<string> writeStatus;
	}
}
