using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.MonitoredUtils
{
	// Token: 0x02000130 RID: 304
	public struct XeInfo : IContainsPrivateInformation
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x0001B71D File Offset: 0x0001991D
		public XeInfo(int tid, DateTime ts, string src)
		{
			this.ThreadId = tid;
			this.Timestamp = ts;
			this.Source = src;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x0001B734 File Offset: 0x00019934
		public string ToPrivateString()
		{
			return this.ToOriginalString();
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x0001B734 File Offset: 0x00019934
		public string ToInternalString()
		{
			return this.ToOriginalString();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x0001B73C File Offset: 0x0001993C
		public string ToOriginalString()
		{
			return "{0},{1:o},{2}".FormatWithInvariantCulture(new object[] { this.ThreadId, this.Timestamp, this.Source });
		}

		// Token: 0x040002E5 RID: 741
		public int ThreadId;

		// Token: 0x040002E6 RID: 742
		public DateTime Timestamp;

		// Token: 0x040002E7 RID: 743
		public string Source;
	}
}
