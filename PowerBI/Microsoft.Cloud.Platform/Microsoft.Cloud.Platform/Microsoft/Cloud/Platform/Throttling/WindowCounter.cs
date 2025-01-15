using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x02000102 RID: 258
	public sealed class WindowCounter
	{
		// Token: 0x06000733 RID: 1843 RVA: 0x00019928 File Offset: 0x00017B28
		public WindowCounter(int count, DateTime windowStartTimeStamp)
		{
			ExtendedDiagnostics.EnsureArgumentIsPositive(count, "WindowCounter count should be greater than zero");
			this.Count = count;
			this.WindowStartTimeStamp = windowStartTimeStamp;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x00019949 File Offset: 0x00017B49
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x00019951 File Offset: 0x00017B51
		public int Count { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001995A File Offset: 0x00017B5A
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00019962 File Offset: 0x00017B62
		public DateTime WindowStartTimeStamp { get; private set; }
	}
}
