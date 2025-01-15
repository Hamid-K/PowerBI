using System;

namespace Microsoft.WindowsAzure.Diagnostics
{
	// Token: 0x02000463 RID: 1123
	[Obsolete("This API is deprecated.")]
	public class PerformanceCounterConfiguration
	{
		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600273E RID: 10046 RVA: 0x000779F0 File Offset: 0x00075BF0
		// (set) Token: 0x0600273F RID: 10047 RVA: 0x000779F8 File Offset: 0x00075BF8
		public string CounterSpecifier { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002740 RID: 10048 RVA: 0x00077A01 File Offset: 0x00075C01
		// (set) Token: 0x06002741 RID: 10049 RVA: 0x00077A09 File Offset: 0x00075C09
		public TimeSpan SampleRate { get; set; }
	}
}
