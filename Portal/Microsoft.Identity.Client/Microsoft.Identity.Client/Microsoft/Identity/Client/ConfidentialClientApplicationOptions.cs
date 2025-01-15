using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000138 RID: 312
	public class ConfidentialClientApplicationOptions : ApplicationOptions
	{
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x0003A33F File Offset: 0x0003853F
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x0003A347 File Offset: 0x00038547
		public string ClientSecret { get; set; }

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x0003A350 File Offset: 0x00038550
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x0003A358 File Offset: 0x00038558
		public string AzureRegion { get; set; }

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x0003A361 File Offset: 0x00038561
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x0003A369 File Offset: 0x00038569
		public bool EnableCacheSynchronization { get; set; } = true;
	}
}
