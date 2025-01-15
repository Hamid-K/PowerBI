using System;
using Microsoft.Identity.Client.Region;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000179 RID: 377
	public class RegionDetails
	{
		// Token: 0x06001261 RID: 4705 RVA: 0x0003EB5A File Offset: 0x0003CD5A
		public RegionDetails(RegionOutcome regionOutcome, string regionUsed, string autoDetectionError)
		{
			this.RegionOutcome = regionOutcome;
			this.RegionUsed = regionUsed;
			this.AutoDetectionError = autoDetectionError;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x0003EB77 File Offset: 0x0003CD77
		public RegionOutcome RegionOutcome { get; }

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0003EB7F File Offset: 0x0003CD7F
		public string RegionUsed { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x0003EB87 File Offset: 0x0003CD87
		public string AutoDetectionError { get; }
	}
}
