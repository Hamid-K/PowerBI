using System;
using System.Collections.Generic;

namespace Microsoft.OData
{
	// Token: 0x0200001B RID: 27
	internal sealed class InstanceAnnotationWriteTracker
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x00003C20 File Offset: 0x00001E20
		public InstanceAnnotationWriteTracker()
		{
			this.writeStatus = new HashSet<string>(StringComparer.Ordinal);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C38 File Offset: 0x00001E38
		public bool IsAnnotationWritten(string key)
		{
			return this.writeStatus.Contains(key);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003C46 File Offset: 0x00001E46
		public bool MarkAnnotationWritten(string key)
		{
			return this.writeStatus.Add(key);
		}

		// Token: 0x04000030 RID: 48
		private readonly HashSet<string> writeStatus;
	}
}
