using System;

namespace Microsoft.Cloud.Platform.Throttling
{
	// Token: 0x0200010F RID: 271
	internal sealed class ThrottlingWindow
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x0001A01B File Offset: 0x0001821B
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x0001A023 File Offset: 0x00018223
		public long NumberOfEvents { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001A02C File Offset: 0x0001822C
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x0001A034 File Offset: 0x00018234
		public long EndTimeInTicks { get; set; }
	}
}
